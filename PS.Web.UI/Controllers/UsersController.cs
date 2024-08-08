using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Entity;
using PS.Web.UI.Model;
using System.Net.NetworkInformation;
using System.Security.Claims;


namespace PS.Web.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Login() {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");

            }
            return View(); 
        }
        public IActionResult Register() { 
        return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepo.Users.FirstOrDefaultAsync(x=> x.UserName == model.UserName || x.UserEmail == model.UserEmail );
                if (user == null)
                {
                    _userRepo.CreateUser(new User {
                         
                        UserName = model.UserName,
                         UserEmail = model.UserEmail,
                         Password = model.Password
                    });
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Email başka bir kullanıcı tarafından kullanılıyor");
                };
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) { 
            if (ModelState.IsValid)
            {
                var isUser = _userRepo.Users.FirstOrDefault(x => x.UserEmail == model.UserEmail && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));

                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));

                    userClaims.Add(new Claim(ClaimTypes.Email, isUser.UserEmail ?? ""));

                    if (isUser.UserEmail == "ilyaskazdal@gmail.com" && isUser.UserId == 1 && isUser.UserName == "ilyas") {

                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    
                    var claimsIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);

                    // sonradan ekle benim cookimi içerde tutsunmu checkboxı
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                  new ClaimsPrincipal(claimsIdentity),
                                                  authProperties);
                    return RedirectToAction("Index", "Home");

                }
                else
            {
                ModelState.AddModelError("","Kullanıcı adı veya Şifreniz yanlış");
            }

            }
           
            return View(model); 
        }

        public ActionResult Profile()
        {
            string username = User.Identity.Name;
            var user = _userRepo.Users.FirstOrDefault(x => x.UserName == username); 
            return View(user);
        }


        public ActionResult Update(int id)
        {
            var user = _userRepo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            
            var viewModel = new UserUpdateViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                Password = user.Password,
                
            };

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Update(UserUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Update user data in the database
            var user = new User
            {
                UserId = viewModel.UserId,
                UserName = viewModel.UserName,
                UserEmail = viewModel.UserEmail,
                Password = viewModel.Password,

            };
            _userRepo.UpdateUser(user);

          
            return RedirectToAction("Login", "Users", new { id = viewModel.UserId });
        }
        public IActionResult Search() { 
            return View();
        }

        [HttpGet]
        [Route("User")]
        public IActionResult Search(String searchString)
        {
           

            var users = from u in _userRepo.Users
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));

                // Check if any user matches the search criteria
                var foundUser = users.FirstOrDefault();
                if (foundUser != null)
                {
                    // Redirect to the user's profile page
                    return RedirectToAction("RelatedInfo", new { id = foundUser.UserId });
                }
            }

            // If no user found or if search string is empty, display the search results
            return View(users.ToList());


        }

        [HttpGet]
        [Route("User/RelatedInfo/{id}")]
        public ActionResult RelatedInfo(int id)
        {
            // Retrieve user from database by id
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound(); // Or handle accordingly if user not found
            }

            // Pass necessary information related to the searched user to the view
            return View(user);
        }
    }
}

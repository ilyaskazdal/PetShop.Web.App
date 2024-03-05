using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PS.Data.Abstract;
using PS.Web.UI.Model;
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

        public IActionResult Index() { 
            return View(); 
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

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserName ?? ""));

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserEmail ?? ""));

                    if (isUser.UserEmail == "ilyaskazdal@gmail.com") {

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
    }
}

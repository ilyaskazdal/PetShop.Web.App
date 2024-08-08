using Microsoft.AspNetCore.Mvc;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Entity;
using PS.Web.UI.Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PS.Web.UI.Controllers
{
    public class AdoptController : Controller
    {

       private readonly IAdoptRepo _adoptrepo;
        private readonly IUserRepo _userrepo;

        public AdoptController(IAdoptRepo adoptrepo,IUserRepo userRepo)
        {
            _adoptrepo = adoptrepo;
            _userrepo = userRepo;
        }

        public IActionResult Index()
        {

            return View(new AdoptViewModel
            {
                Adopts = _adoptrepo.Adopts.ToList()

            });
        }

        public IActionResult Requests() {
            if (User.Identity!.IsAuthenticated)
            {
               return View();

            }
            else
            {
return RedirectToAction("Index", "Home");
            }
            
         
        }

        [HttpPost]
        public IActionResult SendRequest(Adopt  adopt) {

            if (ModelState.IsValid)
            {

                adopt.AdoptId = adopt.AdoptId;
                adopt.AnimalName = Request.Form["AnimalName"].ToString();
                adopt.AnimalDescription = Request.Form["AnimalDescription"].ToString();
                string amac = Request.Form["amac"].ToString();
                string userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
                


                int userIdIntValue = int.Parse(userId);
                
                adopt.UserId = userIdIntValue;
                if (amac == "1")
                {

                    adopt.Gender = true;

                }
                else
                {
                    adopt.Gender = false;

                }
                _adoptrepo.CreateAdoptRequest(adopt);

            }
            return RedirectToAction("Index", "Home");



        }

    }
}

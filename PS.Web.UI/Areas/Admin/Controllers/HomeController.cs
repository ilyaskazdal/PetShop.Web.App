using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PS.Web.UI.Areas.Admins.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();   
        }
    }
}

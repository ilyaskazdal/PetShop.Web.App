using Microsoft.AspNetCore.Mvc;

namespace PS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var claims = User.Claims;

            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace PS.Web.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

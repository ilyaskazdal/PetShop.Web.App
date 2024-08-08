using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Entity;
using System.Drawing.Text;

namespace PS.Web.UI.Controllers
{
    public class ContactController : Controller
    {
        
        private IContactRepo _contactRepo;
        public ContactController(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;

        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Contact contact)
        {
            
            contact.ContactID = contact.ContactID;
            contact.Name = Request.Form["Name"].ToString();
            contact.Surname = Request.Form["Surname"].ToString();
            contact.EMail = Request.Form["EMail"].ToString();
            contact.Message = Request.Form["Message"].ToString();
            contact.CreatedDate = DateTime.Now;
            contact.IsDeleted = false;

               string amac = Request.Form["amac"].ToString();

            
            // 1 kullanıcı geri dönüşü (true) --- 2 işbirliği (false) çalışyor!!

            if (amac == "1") {
                
                contact.Purpose = true;
            
            }
            else
            {
                contact.Purpose = false;

            }


            _contactRepo.CreateContact(contact);
          
            return RedirectToAction("Index", "Home");

        }

    }
}

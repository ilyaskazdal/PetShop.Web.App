using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PS.Data.Abstract;
using PS.Entity;
using PS.Data.Concrete.EfCore;

namespace PS.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactRepo _contactRepo;
        private readonly PSContext _context;

        public ContactController(IContactRepo contactRepo, PSContext context)
        {
            _contactRepo = contactRepo;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contact = _contactRepo.Contacts;
            return View(await contact.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactRepo.Contacts
                                   .FirstOrDefaultAsync(x => x.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (_contactRepo.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _contactRepo.Contacts
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (_contactRepo.Contacts == null)
            {
                return Problem("Entity set 'BlogDbContext.Categories'  is null.");
            }
            var contact =await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

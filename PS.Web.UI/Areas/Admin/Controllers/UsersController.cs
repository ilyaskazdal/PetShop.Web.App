using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Entity;

namespace PS.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly PSContext pSContext;
        private readonly IUserRepo _context;


        public UsersController(IUserRepo context, PSContext pSContext)
        {
            _context = context;
            this.pSContext = pSContext;
        }

        public List<User> Users { get; set; } = new();

        public async Task<IActionResult> Index()
        {
            var users = _context.Users;
            return View(await users.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var users = await _context.Users
                       .FirstOrDefaultAsync(x => x.UserId == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (pSContext.Users == null)
            {
                return Problem("Entity set 'DbContext'  is null.");
            }
            
            var user =await pSContext.Users.FindAsync(id);
            if (user != null)
            {
               pSContext.Users.Remove(user);
            }
          await pSContext.SaveChangesAsync();

         
            return RedirectToAction(nameof(Index));
        }

    }
}

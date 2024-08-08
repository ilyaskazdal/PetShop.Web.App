using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Entity;

namespace PS.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdoptsController : Controller
    {
        private readonly PSContext _dbContext;

        public AdoptsController(PSContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task<IActionResult> Index()
        {
            return _dbContext.Adopts != null ?
                        View(await _dbContext.Adopts.ToListAsync()) :
                        Problem("Entity set 'PSContext.Categories'  is null.");
        }
        public async Task<IActionResult> Details(int id)
        {
            if (_dbContext.Adopts == null)
            {
                return NotFound();
            }

            

            var adopt = await _dbContext.Adopts
                .Include(c=>c.User)
                .FirstOrDefaultAsync(m => m.AdoptId == id);
            if (adopt == null)
            {
                return NotFound();
            }

            return View(adopt);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_dbContext.Adopts == null)
            {
                return NotFound();
            }

            var adopt = await _dbContext.Adopts
                .FirstOrDefaultAsync(m => m.AdoptId == id);
            if (adopt == null)
            {
                return NotFound();
            }

            return View(adopt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.Adopts == null)
            {
                return Problem("Entity set 'BlogDbContext.Categories'  is null.");
            }
            var adopt = await _dbContext.Adopts.FindAsync(id);
            if (adopt != null)
            {
                _dbContext.Adopts.Remove(adopt);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

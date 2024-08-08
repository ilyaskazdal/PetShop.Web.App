using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Data.Concrete.EfCore;

namespace PS.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrdersController : Controller
    {
        private readonly PSContext _context;

        public OrdersController(PSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Orders != null ?
                       View(await _context.Orders.ToListAsync()) :
                       Problem("Entity set 'PSContext.Categories'  is null.");
        }
        public async Task<IActionResult> Details(int id)
        {
            var orders = await _context.Orders
                                   .Include(i=>i.OrderItems)
                                   .FirstOrDefaultAsync(x => x.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
            

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DbContext'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS.Data.Concrete.EfCore;
using PS.Web.UI.Helpers;
using PS.Web.UI.Model;

namespace PS.Web.UI.Pages
{
    public class CartModel : PageModel
    {
        private PSContext _context;

        public CartModel(PSContext context, Cart cartService)
        {
            _context = context;
            Cart = cartService;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            
            if(product != null)
            {

                Cart?.AddItem(product, 1);
            
            }

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRemove(int ProductId)
        {
            Cart?.RemoveItem(Cart.Items.First(p => p.Product.ProductId == ProductId).Product);
            return RedirectToPage("/Cart");
        }
    }
}

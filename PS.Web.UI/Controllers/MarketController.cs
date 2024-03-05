using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using PS.Web.UI.Model;


namespace PS.Web.UI.Controllers
{
    
    public class MarketController : Controller
    {
        
        
        private IProductRepo _productRepository;
        private PSContext _psContext;


        public MarketController(IProductRepo repository,PSContext pSContext)
        {
            _productRepository = repository;
            _psContext = pSContext; 
            
        }

        public IActionResult Index()
        {

            return View(
                new ProductViewModel
                {
                    Products = _productRepository.Products.ToList()

                }
                
                );
          
        }

        public async Task<IActionResult> Details(int id)
        {

            if (id == 0 || _productRepository.Products == null) { 
            return NotFound();
            }
            var product = await _productRepository.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

             
        }

        public async Task<IActionResult> Kategoriler(int id) {
            var category = await _psContext.Categories
                
                .FirstOrDefaultAsync(b=>b.CategoryId==id);
            return View(category);
        }




    }
}

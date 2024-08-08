using Microsoft.AspNetCore.Authorization;
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
        
        
        private readonly IProductRepo _productRepository;
        private readonly PSContext _psContext;


        public MarketController(IProductRepo repository,PSContext pSContext)
        {
            _productRepository = repository;
            _psContext = pSContext; 
            
        }

       
        public IActionResult Index()
        {
            return View(new ProductViewModel
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
           
            var products =await _productRepository.Products.Where(p => p.CategoryId == id).ToListAsync();
  
            return View(products);
        }




    }
}

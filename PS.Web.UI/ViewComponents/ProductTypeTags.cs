using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Data.Abstract;
using PS.Entity;
namespace PS.Web.UI.ViewComponents
{
    public class ProductTypeTags : ViewComponent
    {
        private IProductTypeRepo _productTypeRepo;

        public ProductTypeTags(IProductTypeRepo productTypeRepo)
        {
            _productTypeRepo = productTypeRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View( await
                _productTypeRepo
                .ProductTypes
                .ToListAsync());
            
         
            
            


        }
    }
}

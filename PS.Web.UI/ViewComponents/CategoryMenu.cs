using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using PS.Data.Abstract;

namespace PS.Web.UI.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private ICategoryRepo _categoryRepo;

        public CategoryMenu(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View( await 
                _categoryRepo
                .Categories
                .ToListAsync());

        }

    }
}

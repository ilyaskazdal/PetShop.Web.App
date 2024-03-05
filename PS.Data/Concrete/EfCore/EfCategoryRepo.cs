using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfCategoryRepo : ICategoryRepo
    {
        private PSContext _context;

        public EfCategoryRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;
    }
}

using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfProductRepo : IProductRepo
    {
        private PSContext _context;

        public EfProductRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}

using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfProductTypeRepo: IProductTypeRepo
    {
        private PSContext _context;

        public EfProductTypeRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<ProductType> ProductTypes => _context.ProductTypes;
    }
}

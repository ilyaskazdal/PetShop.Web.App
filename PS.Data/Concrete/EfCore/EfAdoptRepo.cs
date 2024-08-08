using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfAdoptRepo : IAdoptRepo
    {
        private PSContext _context;

        public EfAdoptRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<Adopt> Adopts => _context.Adopts;

        public void CreateAdoptRequest(Adopt adopt)
        {
            _context.Adopts.Add(adopt);
            _context.SaveChanges();
        }
    }
}

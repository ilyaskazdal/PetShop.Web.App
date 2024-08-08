using PS.Data.Abstract;
using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Concrete.EfCore
{
    public class EfOrderRepo : IOrderRepo
    {
        private PSContext _context;

        public EfOrderRepo(PSContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders;

        public void TakeOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}

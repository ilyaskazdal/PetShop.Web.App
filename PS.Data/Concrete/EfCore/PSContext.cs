using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Entity;

namespace PS.Data.Concrete.EfCore
{
   public class PSContext:DbContext
    {
        public PSContext(DbContextOptions<PSContext> options) : base(options)
        {
            

        }

        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Adopt> Adopts => Set<Adopt>();

        public DbSet<Order> Orders => Set<Order>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Entity;


namespace PS.Data.Abstract
{
    public interface IProductRepo
    {
        IQueryable<Product> Products { get; }


    }
}

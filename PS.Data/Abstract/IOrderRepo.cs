﻿using PS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Abstract
{
    public interface IOrderRepo
    {
        IQueryable<Order> Orders { get; }

        void TakeOrder(Order order);
    }
}

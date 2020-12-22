using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabySitter.Models.Dao
{
    public class OrderDao
    {
        BabySitterEntities db = null;
        public OrderDao()
        {
            db = new BabySitterEntities();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabySitter.Models.Dao
{
    public class OrderDetailDao
    {
        BabySitterEntities db = null;
        public OrderDetailDao()
        {
            db = new BabySitterEntities();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
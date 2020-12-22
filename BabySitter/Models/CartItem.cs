using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabySitter.Models
{
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}
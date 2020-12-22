using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;
using BabySitter.Models.Common;
using BabySitter.Models.Dao;

namespace BabySitter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BabySitterEntities db = new BabySitterEntities();
        public ActionResult Index()
        {      
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(4);
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(4);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
    }
}
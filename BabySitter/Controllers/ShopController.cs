using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;

namespace BabySitter.Controllers
{
    public class ShopController : Controller
    {
        BabySitterEntities db = new BabySitterEntities();
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            var item = from t in db.CategoryPaints
                       select t;
            return PartialView(item.ToList());
        }
    }
}
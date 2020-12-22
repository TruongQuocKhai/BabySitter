using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabySitter.Models;
using System.Web.Mvc;
 

namespace BabySitter.Controllers
{
    public class NewsController : Controller
    {
        BabySitterEntities db = new BabySitterEntities();
        // GET: News
        public ActionResult Index()
        {
            var item = from t in db.InformationTravels
                       where t.hide == true
                       orderby t.datebegin descending
                       select t;
            return View(item.ToList());
        }
        //chi tietes baif viet
        public ActionResult chitietbaiviet(int? id)
        {
            var item = from t in db.InformationTravels
                       where t.id_InformationTravel == id
                       select t;
            return View(item.FirstOrDefault());
        }
        //danh muc tin tuc
        public ActionResult danhmuc()
        {
            var item = from t in db.Categories
                       where t.hide == true
                       select t;
            return PartialView(item.ToList());
        }
        public ActionResult tintuctheodanhmuc(int? id)
        {
            var item = from t in db.InformationTravels
                       where t.id_category == id && t.hide == true
                       orderby t.datebegin descending
                       select t;
            return View(item.ToList());
        }
    }
}
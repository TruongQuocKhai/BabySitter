using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BabySitter.Models;
using System.Net;
using System.Net.Mail;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace BabySitter.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        BabySitterEntities db = new BabySitterEntities();
        public ActionResult Index()
        {
            ViewBag.meta = "Home";
            return View();
        }
        public ActionResult Menu()
        {
            var menu = from t in db.db_Menu
                       where t.hide == true
                       orderby t.order
                       select t;
            return PartialView(menu.ToList());
        }
        public ActionResult Service()
        {
            ViewBag.meta = "Service";
            var Service = from t in db.db_ServiceIntro
                          where t.hide == true
                          select t;
            return PartialView(Service.ToList());
        }
        public ActionResult Introduce()
        {
            ViewBag.meta = "Introduce";
            var Introduce = from t in db.db_ServiceIntro
                            where t.hide == true
                            select t;
            return PartialView(Introduce.ToList());
        }

        public ActionResult InformationTravel(string metatitle, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            ViewBag.meta = metatitle;
            var InformationTravel = from t in db.InformationTravels
                                    where t.hide == true
                                    orderby t.datebegin ascending
                                    select t;
            return PartialView(InformationTravel.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Detail(int? id,string meta)
        {
            //ViewBag.meta = "InformationTravel";
            if (id == null)
            {
                return View("Error");
            }
            InformationTravel informationTravel = db.InformationTravels.Find(id);
            if (informationTravel == null)
            {
                return View("Error");
            }
            var Detail = from t in db.InformationTravels
                         where t.meta == meta
                         select t;
            return View(Detail.FirstOrDefault());
        }
        public ActionResult Error()
        {
            return View();
        }
        //private List<db_InformationTrvel> gett(int count)
        //{
        //    return db.db_InformationTrvel.OrderByDescending(a => a.id_informationtravel).Take(count).ToList();
        //}
        //public ActionResult InformationTravel(string metatitle)
        //{

        //    ViewBag.meta = metatitle;
        //    var InformationTravel = gett(8);
        //    return PartialView(InformationTravel);
        //}


            public ActionResult Sent()
        {
            return PartialView();
        }
        public ActionResult ContactDees()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> ContactDees(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("xuanttt0612@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("dongpham1606@gmail.com");  // replace with valid value
                message.Subject = model.Subject;              
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Subject, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "xuanttt0612@gmail.com",  // replace with valid value
                        Password = "Tx01669240211@"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return PartialView(model);
        }
    }
}
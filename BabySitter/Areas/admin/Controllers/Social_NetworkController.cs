using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;

namespace BabySitter.Areas.admin.Controllers
{
    public class Social_NetworkController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        public bool isLogined()
        {
            if (Session[CommonConstant.ADMIN_ID] == null || Session[CommonConstant.ADMIN_PASSWORD] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // GET: admin/Social_Network
        public ActionResult Index()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.Social_Network.ToList());
        }

        // GET: admin/Social_Network/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social_Network social_Network = db.Social_Network.Find(id);
            if (social_Network == null)
            {
                return HttpNotFound();
            }
            return View(social_Network);
        }

        // GET: admin/Social_Network/Create
        public ActionResult Create()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: admin/Social_Network/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_Social_Network,Social_Network1,icon,hide,order")] Social_Network social_Network, HttpPostedFileBase icon)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (icon != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + icon.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        icon.SaveAs(path);
                        social_Network.icon = filename; //Luu ý
                    }
                    else
                    {
                        social_Network.icon = "logo.png";
                    }
                    social_Network.hide = Convert.ToBoolean(true);
                    db.Social_Network.Add(social_Network);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(social_Network);
        }

        // GET: admin/Social_Network/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social_Network social_Network = db.Social_Network.Find(id);
            if (social_Network == null)
            {
                return HttpNotFound();
            }
            return View(social_Network);
        }

        // POST: admin/Social_Network/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Social_Network,Social_Network1,icon,hide,order")] Social_Network social_Network, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Social_Network temp = getById(social_Network.id_Social_Network);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        temp.icon = filename; //Luu ý
                    }
                    //temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.Social_Network1 = social_Network.Social_Network1;
                    temp.order = social_Network.order;
                    temp.hide = social_Network.hide;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(social_Network);
        }
        public Social_Network getById(long id)
        {
            return db.Social_Network.Where(x => x.id_Social_Network == id).FirstOrDefault();
        }

        // GET: admin/Social_Network/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social_Network social_Network = db.Social_Network.Find(id);
            if (social_Network == null)
            {
                return HttpNotFound();
            }
            return View(social_Network);
        }

        // POST: admin/Social_Network/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Social_Network social_Network = db.Social_Network.Find(id);
            db.Social_Network.Remove(social_Network);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

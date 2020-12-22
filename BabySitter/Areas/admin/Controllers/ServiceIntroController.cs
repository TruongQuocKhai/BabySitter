using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;

namespace BabySitter.Areas.admin.Controllers
{
    public class ServiceIntroController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        // GET: admin/ServiceIntro
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

        public ActionResult Index()
        {

            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.db_ServiceIntro.ToList());
        }

        // GET: admin/ServiceIntro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_ServiceIntro db_ServiceIntro = db.db_ServiceIntro.Find(id);
            if (db_ServiceIntro == null)
            {
                return HttpNotFound();
            }
            return View(db_ServiceIntro);
        }

        // GET: admin/ServiceIntro/Create
        public ActionResult Create()
        {

            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: admin/ServiceIntro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_serviceintro,service,intro,hide,datebegin")] db_ServiceIntro serviceIntro)
        {
            if (ModelState.IsValid)
            {
                db.db_ServiceIntro.Add(serviceIntro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceIntro);
        }

        // GET: admin/ServiceIntro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_ServiceIntro db_ServiceIntro = db.db_ServiceIntro.Find(id);
            if (db_ServiceIntro == null)
            {
                return HttpNotFound();
            }
            return View(db_ServiceIntro);
        }

        // POST: admin/ServiceIntro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_serviceintro,service,intro,hide,datebegin")] db_ServiceIntro db_ServiceIntro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(db_ServiceIntro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(db_ServiceIntro);
        }

        // GET: admin/ServiceIntro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_ServiceIntro db_ServiceIntro = db.db_ServiceIntro.Find(id);
            if (db_ServiceIntro == null)
            {
                return HttpNotFound();
            }
            return View(db_ServiceIntro);
        }

        // POST: admin/ServiceIntro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db_ServiceIntro db_ServiceIntro = db.db_ServiceIntro.Find(id);
            db.db_ServiceIntro.Remove(db_ServiceIntro);
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

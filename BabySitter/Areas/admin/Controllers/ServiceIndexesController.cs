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
    public class ServiceIndexesController : Controller
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
        // GET: admin/ServiceIndexes
        public ActionResult Index()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.ServiceIndexes.ToList());
        }

        // GET: admin/ServiceIndexes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIndex serviceIndex = db.ServiceIndexes.Find(id);
            if (serviceIndex == null)
            {
                return HttpNotFound();
            }
            return View(serviceIndex);
        }

        // GET: admin/ServiceIndexes/Create
        public ActionResult Create()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: admin/ServiceIndexes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_serviceindex,Terms_Condition,Brief_Introduction,Special_Requirements,hide")] ServiceIndex serviceIndex)
        {
            if (ModelState.IsValid)
            {
                db.ServiceIndexes.Add(serviceIndex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceIndex);
        }

        // GET: admin/ServiceIndexes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIndex serviceIndex = db.ServiceIndexes.Find(id);
            if (serviceIndex == null)
            {
                return HttpNotFound();
            }
            return View(serviceIndex);
        }

        // POST: admin/ServiceIndexes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_serviceindex,Terms_Condition,Brief_Introduction,Special_Requirements,hide")] ServiceIndex serviceIndex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceIndex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceIndex);
        }

        // GET: admin/ServiceIndexes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIndex serviceIndex = db.ServiceIndexes.Find(id);
            if (serviceIndex == null)
            {
                return HttpNotFound();
            }
            return View(serviceIndex);
        }

        // POST: admin/ServiceIndexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceIndex serviceIndex = db.ServiceIndexes.Find(id);
            db.ServiceIndexes.Remove(serviceIndex);
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

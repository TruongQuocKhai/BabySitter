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
    public class ProvincesController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        // GET: admin/Provinces
        public ActionResult Index()
        {
            var provinces = db.Provinces.Include(p => p.Area);
            return View(provinces.ToList());
        }

        // GET: admin/Provinces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: admin/Provinces/Create
        public ActionResult Create()
        {
            ViewBag.id_areas = new SelectList(db.Areas, "id_areas", "name_areas");
            return View();
        }

        // POST: admin/Provinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_province,id_areas,nameProvince,hide")] Province province)
        {
            if (ModelState.IsValid)
            {
                province.hide = Convert.ToBoolean(true);
                db.Provinces.Add(province);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_areas = new SelectList(db.Areas, "id_areas", "name_areas", province.id_areas);
            return View(province);
        }

        // GET: admin/Provinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_areas = new SelectList(db.Areas, "id_areas", "name_areas", province.id_areas);
            return View(province);
        }

        // POST: admin/Provinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_province,id_areas,nameProvince,hide")] Province province)
        {
            if (ModelState.IsValid)
            {
                db.Entry(province).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_areas = new SelectList(db.Areas, "id_areas", "name_areas", province.id_areas);
            return View(province);
        }

        // GET: admin/Provinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: admin/Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Province province = db.Provinces.Find(id);
            db.Provinces.Remove(province);
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

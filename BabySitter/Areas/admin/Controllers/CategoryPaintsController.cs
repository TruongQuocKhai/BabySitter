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
    public class CategoryPaintsController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        // GET: admin/CategoryPaints
        public ActionResult Index()
        {
            return View(db.CategoryPaints.ToList());
        }

        // GET: admin/CategoryPaints/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPaint categoryPaint = db.CategoryPaints.Find(id);
            if (categoryPaint == null)
            {
                return HttpNotFound();
            }
            return View(categoryPaint);
        }

        // GET: admin/CategoryPaints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/CategoryPaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,ShowOnHome,Language")] CategoryPaint categoryPaint)
        {
            if (ModelState.IsValid)
            {
                db.CategoryPaints.Add(categoryPaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryPaint);
        }

        // GET: admin/CategoryPaints/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPaint categoryPaint = db.CategoryPaints.Find(id);
            if (categoryPaint == null)
            {
                return HttpNotFound();
            }
            return View(categoryPaint);
        }

        // POST: admin/CategoryPaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,ShowOnHome,Language")] CategoryPaint categoryPaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryPaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryPaint);
        }

        // GET: admin/CategoryPaints/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPaint categoryPaint = db.CategoryPaints.Find(id);
            if (categoryPaint == null)
            {
                return HttpNotFound();
            }
            return View(categoryPaint);
        }

        // POST: admin/CategoryPaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CategoryPaint categoryPaint = db.CategoryPaints.Find(id);
            db.CategoryPaints.Remove(categoryPaint);
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

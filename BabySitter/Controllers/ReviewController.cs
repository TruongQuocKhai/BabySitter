using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;



namespace BabySitter.Controllers
{
    public class ReviewController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        // GET: db_Review
        public ActionResult Index()
        {
            var db_Review = db.db_Review.Include(d => d.Star);
            return View(db_Review.ToList());
        }
      
        // GET: db_Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_Review db_Review = db.db_Review.Find(id);
            if (db_Review == null)
            {
                return HttpNotFound();
            }
            return View(db_Review);
        }


        // GET: db_Review/Create
        public ActionResult Create()
        {
            ViewBag.id_star = new SelectList(db.Stars, "id_star", "StarReview");
            return PartialView();
        }

        // POST: db_Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_review,name_review,title_review,mail_review,phone_review,id_star,mess_review,hide,order,datebegin")] db_Review db_Review, HttpPostedFileBase name)
        {
            if (ModelState.IsValid)
            {
                db_Review.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db_Review.hide = Convert.ToBoolean(true);
                db.db_Review.Add(db_Review);
                db.SaveChanges();
                return RedirectToAction("Index","Main");
            }

            ViewBag.id_star = new SelectList(db.Stars, "id_star", "StarReview", db_Review.id_star);
            return View(db_Review);
        }

        // GET: db_Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_Review db_Review = db.db_Review.Find(id);
            if (db_Review == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_star = new SelectList(db.Stars, "id_star", "StarReview", db_Review.id_star);
            return View(db_Review);
        }

        // POST: db_Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_review,name_review,title_review,mail_review,phone_review,id_star,mess_review,hide,order,datebegin")] db_Review db_Review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(db_Review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_star = new SelectList(db.Stars, "id_star", "StarReview", db_Review.id_star);
            return View(db_Review);
        }

        // GET: db_Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db_Review db_Review = db.db_Review.Find(id);
            if (db_Review == null)
            {
                return HttpNotFound();
            }
            return View(db_Review);
        }

        // POST: db_Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db_Review db_Review = db.db_Review.Find(id);
            db.db_Review.Remove(db_Review);
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

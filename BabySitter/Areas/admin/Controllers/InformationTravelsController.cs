
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;

using BabySitter.Help;
using System.IO;

namespace BabySitter.Areas.admin.Controllers
{
    public class InformationTravelsController : Controller
    {
        private BabySitterEntities db = new BabySitterEntities();

        // GET: admin/InformationTravels
        public ActionResult Index()
        {
            var informationTravels = db.InformationTravels.Include(i => i.Category).Include(i => i.Province);
            return View(informationTravels.ToList());
        }

        // GET: admin/InformationTravels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationTravel informationTravel = db.InformationTravels.Find(id);
            if (informationTravel == null)
            {
                return HttpNotFound();
            }
            return View(informationTravel);
        }

        // GET: admin/InformationTravels/Create
        public ActionResult Create()
        {
            ViewBag.id_category = new SelectList(db.Categories, "id_category", "name_category");
            ViewBag.id_province = new SelectList(db.Provinces, "id_province", "nameProvince");
            return View();
        }

        // POST: admin/InformationTravels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_InformationTravel,id_province,id_category,title,img,description,detail,meta,hide,datebegin")] InformationTravel informationTravel, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img !=null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        informationTravel.img = filename; //Luu ý
                    }
                    else
                    {
                        informationTravel.img = "logo.png";
                    }
                    informationTravel.hide = Convert.ToBoolean(true);
                    informationTravel.datebegin = Convert.ToDateTime(DateTime.Now.ToString());
                    informationTravel.meta = Functions.ConvertToUnSign(informationTravel.title);
                    db.InformationTravels.Add(informationTravel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            ViewBag.id_category = new SelectList(db.Categories, "id_category", "name_category", informationTravel.id_category);
            ViewBag.id_province = new SelectList(db.Provinces, "id_province", "nameProvince", informationTravel.id_province);
            return View(informationTravel);

        }

        // GET: admin/InformationTravels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationTravel informationTravel = db.InformationTravels.Find(id);
            if (informationTravel == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_category = new SelectList(db.Categories, "id_category", "name_category", informationTravel.id_category);
            ViewBag.id_province = new SelectList(db.Provinces, "id_province", "nameProvince", informationTravel.id_province);
            return View(informationTravel);
        }

        // POST: admin/InformationTravels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public InformationTravel getById(long id)
        {
            return db.InformationTravels.Where(x => x.id_InformationTravel == id).FirstOrDefault();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_InformationTravel,id_province,id_category,title,img,description,detail,meta,hide,datebegin")] InformationTravel informationTravel, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                InformationTravel temp = getById(informationTravel.id_InformationTravel);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Luu ý
                    }

                    temp.title = informationTravel.title;
                    temp.description = informationTravel.description;
                    temp.detail = informationTravel.detail;
                    temp.meta = Functions.ConvertToUnSign(informationTravel.title); //convert Ti?ng Vi?t không d?u
                    temp.hide = informationTravel.hide;
                    //temp.img = informationTravel.img;
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
            ViewBag.id_category = new SelectList(db.Categories, "id_category", "name_category", informationTravel.id_category);
            ViewBag.id_province = new SelectList(db.Provinces, "id_province", "nameProvince", informationTravel.id_province);
            return View(informationTravel);
        }

        // GET: admin/InformationTravels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationTravel informationTravel = db.InformationTravels.Find(id);
            if (informationTravel == null)
            {
                return HttpNotFound();
            }
            return View(informationTravel);
        }

        // POST: admin/InformationTravels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformationTravel informationTravel = db.InformationTravels.Find(id);
            db.InformationTravels.Remove(informationTravel);
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

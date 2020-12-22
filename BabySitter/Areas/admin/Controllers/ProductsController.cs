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
using BabySitter.Help;
using BabySitter.Models;

namespace BabySitter.Areas.admin.Controllers
{
    public class ProductsController : Controller
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
        public bool isLoginSucess(string id, string password)
        {
            //Check user's id and password
            foreach (LoginAdm a in db.LoginAdms)
            {
                if (a.Useradm.Replace(" ", "") == id)
                {
                    if (a.Password.Replace(" ", "") == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // GET: admin/Products
        public ActionResult Index()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            var products = db.Products.Include(p => p.CategoryPaint);
            return View(products.ToList());
        }

        // GET: admin/Products/Details/5
        public ActionResult Details(long? id)
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/Products/Create
        public ActionResult Create()
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryPaints, "ID", "Name");
            return View();
        }

        // POST: admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludedVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        product.Image = filename; //Luu ý
                    }
                    else
                    {
                        product.Image = "logo.png";
                    }
                    product.Status = Convert.ToBoolean(true);
                    product.MetaTitle = Functions.ConvertToUnSign(product.Name);
                    product.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString());

                    db.Products.Add(product);
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
            ViewBag.CategoryID = new SelectList(db.CategoryPaints, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: admin/Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.CategoryPaints, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public Product getById(long id)
        {
            return db.Products.Where(x => x.ID == id).FirstOrDefault();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludedVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Product temp = getById(product.ID);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload"), filename);
                        img.SaveAs(path);
                        temp.Image = filename; //Luu ý
                    }
                    temp.Status = Convert.ToBoolean(true);
                    temp.MetaTitle = Functions.ConvertToUnSign(product.Name);
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
            ViewBag.CategoryID = new SelectList(db.CategoryPaints, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: admin/Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

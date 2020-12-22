using BabySitter.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabySitter.Models;
using System.Web.Mvc;
using PagedList;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace BabySitter.Controllers
{
    public class ProductController : Controller
    {
        BabySitterEntities db = new BabySitterEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult NameCategory(int? id)
        {
            var item = from t in db.CategoryPaints
                       where t.ID == id
                       select t;
            return PartialView(item.ToList());
        }

        public ActionResult sanphamlienquan(int? id)
        {
            var item = from t in db.Products
                       select t;
            return PartialView(item.ToList().Take(12));
        }

        public ActionResult AllProduct(int? id, int? page, string meta)
        {
            int pageSize = 20;
            int pageNum = (page ?? 1);
            var item = from t in db.Products
                       where t.Status == true
                       orderby t.CreatedDate ascending
                       select t;
            return PartialView(item.ToPagedList(pageNum, pageSize));
        }
        private List<Product> List8ProductNew(int count)
        {
            return db.Products.OrderByDescending(a => a.CreatedDate).Take(count).ToList();
        }
        public ActionResult List8ProductNew()
        {
            var travel = List8ProductNew(8);
            return PartialView(travel);
        }

        public ActionResult Fashion()
        {
            var item = from t in db.CategoryPaints
                       where t.Status == true
                       select t;
            return PartialView(item);
        }
        public ActionResult ListProduct(int? id, int? page, string meta)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var item = from t in db.Products
                                    where t.CategoryID == id 
                                    orderby t.CreatedDate ascending
                                    select t;
            return PartialView(item.ToPagedList(pageNum, pageSize));
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId, int page = 1, int pageSize = 1)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

      
        public ActionResult Detail(long? id, string meta)
        {
            //ViewBag.meta = "InformationTravel";
            if (id == null)
            {
                return View("Error");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return View("Error");
            }
            var Detail = from t in db.Products
                         where t.MetaTitle == meta
                         select t;
            return View(Detail.FirstOrDefault());
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
                var body = "<p>Email From: {0} ({1})</p><p>Số điện thoại:</p><p>{2}</p><p>Tin nhắn:</p><p>{4}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("xulvin2202@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("ltee.yang@gmail.com");  // replace with valid value
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.NumberPhone, model.Subject, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "ltee.yang@gmail.com",  // replace with valid value
                        Password = "trgiang0010"  // replace with valid value
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
        public ActionResult Sent()
        {
            return PartialView();
        }

        public ActionResult Seller()
        {
            var item = from t in db.Products
                       orderby t.Price descending
                       select t;
            return PartialView(item.ToList().Take(12));
        }


    }
}
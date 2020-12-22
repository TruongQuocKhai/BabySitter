using BabySitter.Models.Dao;
using BabySitter.Models;
using BabySitter.Models.Login;
using BabySitter.Models.Common;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BabySitter.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        BabySitterEntities db = new BabySitterEntities();

        public ActionResult Acc()
        {
            Session[CommonConstantUser.USER_ID] = true;
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SigninSuccess()
        {
            if (Session[CommonConstantUser.USER_ID] == null)
            {
                return PartialView("SigninNull");
            }
            else
            {
                var item = from t in db.Users
                           select t;
                return PartialView(item.SingleOrDefault());
            }
        }


        public ActionResult SigninNull()
        {
            return PartialView();
        }
        //public ActionResult SigninSuccess()
        //{

        //    var acc = from t in db.Users
        //              select t;
        //    return PartialView(acc.SingleOrDefault());
        //}

        public bool isLoginUser()
        {
            if (Session[CommonConstantUser.USER_ID] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isLogined()
        {
            if (Session[CommonConstantUser.USER_ID] == null || Session[CommonConstantUser.USER_PASSWORD] == null)
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
            foreach (User a in db.Users)
            {
                if (a.UserName.Replace(" ", "") == id)
                {
                    if (a.Password.Replace(" ", "") == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public ActionResult Login(string id, string password)
        {
            if (isLogined() == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (id != null || password != null)
            {
                if (isLoginSucess(id, Encryptor.MD5Hash(password)) == true)
                {
                    Session[CommonConstantUser.MESSAGE_USER] = null;
                    Session.Add(CommonConstantUser.USER_ID, id);
                    Session.Add(CommonConstantUser.USER_PASSWORD, password);
                    //Check if Admin's Avatar is exist
                    if (System.IO.File.Exists(
                        HttpContext.Server.MapPath(
                            "~/Areas/admin/Content/images/avatar/" + Session[CommonConstantUser.USER_ID] + ".png")))
                    {
                        Session[CommonConstantUser.USER_AVATA] = string.Format(Session[CommonConstantUser.USER_ID] + ".png");
                    }
                    else
                    {
                        //If not exist show the default avatar
                        Session[CommonConstantUser.USER_AVATA] = "default/_Default.png";
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session[CommonConstantUser.MESSAGE_USER] = "Tài khoản hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstantUser.USER_ID] = null;
            Session[CommonConstantUser.USER_PASSWORD] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //[CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    if (!string.IsNullOrEmpty(model.ProvinceID))
                    {
                        user.ProvinceID = int.Parse(model.ProvinceID);
                    }
                    if (!string.IsNullOrEmpty(model.DistrictID))
                    {
                        user.DistrictID = int.Parse(model.DistrictID);
                    }

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabySitter.Models;
using BabySitter.Help;


namespace BabySitter.Areas.admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: admin/Default
        BabySitterEntities db = new BabySitterEntities();

        
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

        public ActionResult Dashboard()
        {
            //Check if Session is available
            if (isLogined() == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            var item = from t in db.LoginAdms
                       select t;
            return View(item.SingleOrDefault());
        }
        public ActionResult Login(string id, string password)
        {
            if (isLogined() == true)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (id != null || password != null)
            {
                if (isLoginSucess(id, password) == true)
                {
                    Session[CommonConstant.MESSAGE] = null;
                    Session.Add(CommonConstant.ADMIN_ID, id);
                    Session.Add(CommonConstant.ADMIN_PASSWORD, password);
                    //Check if Admin's Avatar is exist
                    if (System.IO.File.Exists(
                        HttpContext.Server.MapPath(
                            "~/Areas/admin/Content/images/avatar/" + Session[CommonConstant.ADMIN_ID] + ".png")))
                    {
                        Session[CommonConstant.ADMIN_AVATAR] = string.Format(Session[CommonConstant.ADMIN_ID] + ".png");
                    }
                    else
                    {
                        //If not exist show the default avatar
                        Session[CommonConstant.ADMIN_AVATAR] = "default/_Default.png";
                    }
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    Session[CommonConstant.MESSAGE] = "UserID or Password is not match.";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstant.ADMIN_ID] = null;
            Session[CommonConstant.ADMIN_PASSWORD] = null;
            return RedirectToAction("Dashboard", "Admin");
        }

    }
}
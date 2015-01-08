using Eclipse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eclipse.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        public ActionResult Index(string Name, string Password)
        {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck != null)
                return View();

            if (Name == null || Password == null)
                return null;

            LoginModel credentials = new Db().getCredentials();
            
            if (Name.Equals(credentials.Name) && Password.Equals(credentials.Password)) 
            {
                var cookie = new HttpCookie("is_logged")
                {
                    Name = "is_logged",
                    Value = DateTime.Now.ToString("dd.MM.yyyy"),
                    Expires = DateTime.Now.AddMinutes(120),
                };
                Response.SetCookie(cookie);

                return View();
            }
            else
                return null;
        }

        public ActionResult Add() {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck == null)
                RedirectToAction("Login");
            
            return View();
        }

        public ActionResult Edit()
        {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck == null)
                RedirectToAction("Login");

            int ID = Convert.ToInt32(RouteData.Values["ID"]);
            ArticleItemModel model = new Db().GetPostById(ID);
            return View(model);
        }

        public ActionResult See() {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck == null)
                RedirectToAction("Login");

            var model = new Db().GetPosts();
            return View(model);
        }

    }
}

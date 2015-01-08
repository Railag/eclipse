using Eclipse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eclipse.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        [HttpPost]
        public ActionResult AddPost(ArticleItemModel post)
        {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck == null)
                RedirectToAction("Login");
            new Db().addPost(post);
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult UpdatePost(ArticleItemModel post)
        {
            var cookieCheck = Request.Cookies["is_logged"];
            if (cookieCheck == null)
                RedirectToAction("Login");
            new Db().updatePost(post);
            return RedirectToAction("Index", "Login");
        }

    }
}

﻿using Eclipse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eclipse.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //var model = new ArticleModel();
            //var model = new Db().GetPostByTitle("title1"); 
            var model = new Db().GetLastPost();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CommentItemModel comment)
        {
            Db db = new Db();
            
            if(comment != null)
                db.addComment(comment);

            return View(db.GetLastPost());
        }

        public ActionResult All()
        {
            var model = new Db().GetPosts();
            return View(model);
        }

    }
}

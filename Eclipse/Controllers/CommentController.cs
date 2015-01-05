using Eclipse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eclipse.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index()
        {
            var model = new CommentModel();
            return View(model);
        }

        public ActionResult Recent()
        {
            var model = new Db().getRecentComments();
            return View(model);
        }

    }
}

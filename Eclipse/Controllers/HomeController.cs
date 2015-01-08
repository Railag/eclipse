using Eclipse.Models;
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

        [HttpGet]
        public ActionResult Index()
        {
            Db db = new Db();
            string title = null;
            if (RouteData.Values.ContainsKey("title"))
                title = RouteData.Values["title"].ToString();
            ArticleItemModel model;
            model = String.IsNullOrWhiteSpace(title) ? db.GetLastPost() : db.GetPostByTitle(title);
            if (model == null)
                return View(db.GetLastPost());
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CommentItemModel NewComment)
        {
            Db db = new Db();

            if (NewComment != null)
                db.addComment(NewComment);

            return View(db.GetLastPost());
        }

        public ActionResult Admin(string submit, int? post, int? comment)
        {
            int id;
            switch (submit)
            {
                case "DeletePost":
                    id = post.Value;
                    new Db().deletePost(id);
                    break;
                case "EditPost":
                    id = post.Value;
                    return RedirectToAction("Edit", "Login", new { ID = id });
                    break;
                case "DeleteComment":
                    id = comment.Value;
                    new Db().deleteComment(id);
                    break;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult All()
        {
            var model = new Db().GetPosts();
            if (model.Articles.Count < ArticleModel.POSTS_PER_PAGE)
                model.CurrentLastPage = model.Articles.Count;
            else
                model.CurrentLastPage = ArticleModel.POSTS_PER_PAGE;
            return View(model);
        }

        [HttpPost]
        public ActionResult All(string submit, int CurrentLastPage)
        {
            switch (submit)
            {
                case "Left":
                    CurrentLastPage -= 10;
                    if (CurrentLastPage < ArticleModel.POSTS_PER_PAGE)
                        CurrentLastPage = ArticleModel.POSTS_PER_PAGE;
                    break;
                case "Right":
                    CurrentLastPage += 10;
                    break;
            }
            var model = new Db().GetPosts();
            if (model.Articles.Count < CurrentLastPage)
                model.CurrentLastPage = model.Articles.Count;
            else
                model.CurrentLastPage = CurrentLastPage;
            return View(model);
        }

    }
}

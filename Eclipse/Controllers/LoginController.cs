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
            LoginModel credentials = new Db().getCredentials();

            if (Name.Equals(credentials.Name) && Password.Equals(credentials.Password))
                return View();
            else
                return null;
        }

    }
}

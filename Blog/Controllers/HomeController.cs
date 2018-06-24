using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.DAO;
using DataAccess.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            ArticleDao aDao = new ArticleDao();
            
            return View(aDao.GetAll());
        }

        [HttpPost]
        public ActionResult SignIn(string nick, string pass)
        {
            if (Membership.ValidateUser(nick, pass))
            {
                FormsAuthentication.SetAuthCookie(nick, false);
                TempData["message-success"] = "Úspěšně přihlášen";
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Nick nebo heslo neni spravne";
            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
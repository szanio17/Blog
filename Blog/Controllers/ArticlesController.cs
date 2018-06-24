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
    public class ArticlesController : Controller
    {

        // GET: Articles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int articleId)
        { 
            ArticleDao aDao = new ArticleDao();
            CommentDao cDao = new CommentDao();
            Article article = aDao.GetById(articleId);
            ViewBag.CommentsCount = cDao.GetByArticleId(article).Count.ToString();
            User user = Membership.GetUser(User.Identity.Name) as User;
            if (user != null && User.Identity.IsAuthenticated)
            {
                if (user.Id == article.AuthorId)
                {
                    ViewBag.canEdit = true;
                }
                else
                {
                    if (User.IsInRole("Admin"))
                    {
                        ViewBag.canEdit = true;
                    }
                    else
                    {
                        ViewBag.canEdit = false;
                    }
                    
                }

            }
            else
            {
                ViewBag.canEdit = false;
            }

            return View(article);
        }

        
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add(Article article)
        {
            if (ModelState.IsValid)
            {
                ArticleDao aDao = new ArticleDao();
                User user = Membership.GetUser(User.Identity.Name) as User;
                article.AuthorId = user.Id;
                article.AuthorName = user.FirstName + " " + user.LastName;
                aDao.Create(article);
                TempData["message-success"] = "Příspěvek byl úspěšně publikován";
            }
            else
            {
                return View("Create", article);
            }

            return RedirectToAction("Index", "Home");
            
        }
        [Authorize]
        public ActionResult Edit(int articleId)
        {
            ArticleDao aDao = new ArticleDao();
            Article article = aDao.GetById(articleId);
            return View(article);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Update(Article article)
        {
            if (ModelState.IsValid)
            {
                ArticleDao aDao = new ArticleDao();
                aDao.Update(article);
                TempData["message-success"] = "Článek " + article.Title + " byl upraven";
            }
            else
            {
                return View("Edit", article);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
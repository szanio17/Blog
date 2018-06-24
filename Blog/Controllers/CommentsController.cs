using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DataAccess.DAO;
using DataAccess.Models;

namespace Blog.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CommentsForArticle(int articleId)
        {
            ArticleDao aDao = new ArticleDao();
            Article article = aDao.GetById(articleId);
            ViewBag.ArticleTitle = article.Title;
            ViewBag.ArticleId = article.Id;
            CommentDao cDao = new CommentDao();

            return View(cDao.GetByArticleId(article));
        }

        public ActionResult Create(int articleId)
        {
            User user = Membership.GetUser(User.Identity.Name) as User;
            ViewBag.ArticleId = articleId;
            if(user != null && User.Identity.IsAuthenticated )
                return View(new Comment(){AuthorEmail = user.Email, AuthorNick = user.Nick});
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add(Comment comment, int articleId)
        {
            string articleTitle;
            if (ModelState.IsValid)
            {
                CommentDao cDao = new CommentDao();
                ArticleDao aDao = new ArticleDao();
                
                Article article = aDao.GetById(articleId);
                comment.Article = article;
                articleTitle = article.Title;
                comment.IpAddress = Request.UserHostAddress;
                comment.UserAgent = Request.UserAgent;
                if (User != null && User.Identity.IsAuthenticated)
                {
                    User user = Membership.GetUser(User.Identity.Name) as User;
                    comment.AuthorNick = user.Nick;
                    comment.AuthorEmail = user.Email;
                }
                cDao.Create(comment);
                TempData["message-success"] = "Příspěvek do diskuze byl úspěšně přidán";
            }
            else
            {
                return View("Create", comment);
            }

            try
            {
                SendNotificationEmail(comment.AuthorEmail, articleTitle);
            }
            catch (Exception e)
            {
                TempData["error"] = e;
            }
            return RedirectToAction("CommentsForArticle", new RouteValueDictionary(
                new { controller = "Comments", action = "CommentsForArticle", articleId = articleId }));
        }
        [Authorize(Roles =  "Admin")]
        public ActionResult Edit(int commentId)
        {
            CommentDao cDao = new CommentDao();
            Comment comment = cDao.GetById(commentId);
            return View(comment);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Update(Comment comment,int articleId)
        {
            if (ModelState.IsValid)
            {
                CommentDao cDao = new CommentDao();
                ArticleDao aDao = new ArticleDao();
                comment.Article = aDao.GetById(articleId);
                cDao.Update(comment);
                TempData["message-success"] = "Komentář byl upraven";
            }
            else
            {
                return View("Edit", comment);
            }

            return RedirectToAction("CommentsForArticle", new RouteValueDictionary(
                new { controller = "Comments", action = "CommentsForArticle", articleId = comment.Article.Id }));
        }

        private void SendNotificationEmail(string email, string articleTitle)
        {
            using (SmtpClient client = new SmtpClient("smtp.elasticemail.com",2525)
                { Credentials = new NetworkCredential("testovaci-projekt@walmark.cz","902e8636-b683-4bed-9749-857b11257c0b")})
            {
                MailMessage mess = new MailMessage();
                mess.From = new MailAddress("testovaci-projekt@walmark.cz");
                mess.To.Add(new MailAddress(email));
                mess.Subject = "Příspěvek do diskuze";
                mess.Body = "Přidal jste komentář k článku " + articleTitle;
                client.Send(mess);
            }
        }
        
    }
}
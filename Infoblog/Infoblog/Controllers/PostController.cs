using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infoblog.Models;

namespace Infoblog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowPost()
        {
            var ctx = new ApplicationDbContext();
            var pvm = new FormalPostViewModel();
            pvm.Posts = ctx.Post.ToList();

            return View(pvm);
        }

        public ActionResult WritePost(FormalPostViewModel postmodel)
        {
            var ctx = new ApplicationDbContext();
            var post = new FormalPostModel();
            post.Title = postmodel.Title;
            post.Content = postmodel.Content;
            post.Author = postmodel.Author;
            ctx.Post.Add(post);
            ctx.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
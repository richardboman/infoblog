using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infoblog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infoblog.Controllers
{
    [Authorize]
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
            pvm.Posts = ctx.Post.OrderByDescending(p => p.Date).ToList();

            return View(pvm);
        }

        public ActionResult WritePost(FormalPostViewModel postmodel)
        { 
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(userId);
            var post = new FormalPostModel();
            post.Title = postmodel.Title;
            post.Content = postmodel.Content;
            post.Author = user.FirstName + " " + user.LastName;
            post.Date = DateTime.Now;
            ctx.Post.Add(post);
            ctx.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
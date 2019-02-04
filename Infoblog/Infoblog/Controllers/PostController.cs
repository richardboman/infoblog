using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (!ModelState.IsValid)
            {
                postmodel.Posts = ctx.Post.OrderByDescending(p => p.Date).ToList();
                return View("ShowPost", postmodel);
            }

            if (postmodel.File != null && postmodel.File.ContentLength > 0 )
            {
                var fileName = Path.GetFileName(postmodel.File.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                post.FilePath = "~/UploadedFiles/" + fileName;
                postmodel.File.SaveAs(path);
            }

                ctx.Post.Add(post);
                ctx.SaveChanges();

            return RedirectToAction("ShowPost");
        }
    }
}

   
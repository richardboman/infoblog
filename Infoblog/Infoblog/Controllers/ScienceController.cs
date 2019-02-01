using Infoblog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    [Authorize]
    public class ScienceController : Controller
    {
        // GET: Science
        public ActionResult SciencePostView()
        {
            var ctx = new ApplicationDbContext();
            var posts = ctx.SciencePost.OrderByDescending(sp => sp.Date).ToList();
            var postViewModels = new List<PostViewModel>();

            foreach(var p in posts)
            {
                var user = ctx.Users.SingleOrDefault(u => u.Email == p.Author);
                postViewModels.Add(new PostViewModel()
                {
                    Id = p.Id,
                    Author = p.Author,
                    Content = p.Content,
                    Title = p.Title,
                    Date = p.Date,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            return View(postViewModels);
        }

        public ActionResult AddSciencePost(ScienceModel post)
        {

            var ctx = new ApplicationDbContext();
            ctx.SciencePost.Add(post);
            ctx.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult EditPost(PostViewModel post)
        {
            return View(post);
        }

        [HttpPost]
        [ActionName("EditPost")]
        public virtual ActionResult SaveEdit(PostViewModel post)
        {
            var ctx = new ApplicationDbContext();
            var postToEdit = ctx.SciencePost.SingleOrDefault(p => p.Id == post.Id);
            postToEdit.Content = post.Content;
            postToEdit.Title = post.Title;
            ctx.SaveChanges();
            return RedirectToAction("SciencePostView", "Science");
        }

        public ActionResult NewSciencePostPartial()
        {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(userId);
            var model = new ScienceModel
            {
                Author = User.Identity.Name,
                Date = DateTime.Now
            };

            return PartialView("NewSciencePostPartial", model);
        }
    }

}
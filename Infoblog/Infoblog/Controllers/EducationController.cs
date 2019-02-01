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
    public class EducationController : Controller
    {
        // GET: Education
        public ActionResult EducationPostView()
        {
            var ctx = new ApplicationDbContext();
            var posts = ctx.EducationPosts.OrderByDescending(p => p.Date).ToList();
            var postViewModels = new List<PostViewModel>();
            foreach(var post in posts)
            {
                var user = ctx.Users.SingleOrDefault(u => u.Email == post.Author);
                postViewModels.Add(new PostViewModel()
                {
                    Id = post.Id,
                    Author = post.Author,
                    Content = post.Content,
                    Title = post.Title,
                    Date = post.Date,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            return View(postViewModels);
        }

        [HttpPost]
        public ActionResult Post(EducationPostModel post)
        {

            var ctx = new ApplicationDbContext();
            ctx.EducationPosts.Add(post);
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
            var postToEdit = ctx.EducationPosts.SingleOrDefault(p => p.Id == post.Id);
            postToEdit.Content = post.Content;
            postToEdit.Title = post.Title;
            ctx.SaveChanges();
            return RedirectToAction("EducationPostView", "Education");
        }

        public ActionResult RemovePost(PostViewModel postmodel)
        {
            var ctx = new ApplicationDbContext();
            var post = ctx.EducationPosts.SingleOrDefault(p => p.Id == postmodel.Id);
            if (post != null)
            {
                var postToRemove = ctx.EducationPosts.Find(postmodel.Id);
                ctx.EducationPosts.Remove(postToRemove);
                ctx.SaveChanges();
            }
            
            return RedirectToAction("EducationPostView", "Education");
        }

        public ActionResult _NewPostPartial()
        {
            var model = new EducationPostModel
            {
                Author = User.Identity.Name,
                Date = DateTime.Now
            };
            return PartialView("_NewPostPartial", model);
        }
    }
}
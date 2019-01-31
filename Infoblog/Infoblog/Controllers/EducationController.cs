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
            var model = ctx.EducationPosts.OrderByDescending(p => p.Date).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Post(EducationPostModel post)
        {

            var ctx = new ApplicationDbContext();
            ctx.EducationPosts.Add(post);
            ctx.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult _NewPostPartial()
        {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(userId);
            var model = new EducationPostModel
            {

                Author = user.FirstName + " " + user.LastName,
            Date = DateTime.Now
            };
            return PartialView("_NewPostPartial", model);
        }
    }
}
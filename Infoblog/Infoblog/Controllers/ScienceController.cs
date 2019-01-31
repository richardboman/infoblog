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
    public class ScienceController : Controller
    {
        // GET: Science
        public ActionResult SciencePostView()
        {
            var ctx = new ApplicationDbContext();
            var model = ctx.SciencePost.OrderByDescending(sp => sp.Date).ToList();
            return View(model);
        }

        public ActionResult AddSciencePost(ScienceModel post)
        {

            var ctx = new ApplicationDbContext();
            ctx.SciencePost.Add(post);
            ctx.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
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
                Author = user.FirstName + " " + user.LastName,
                Date = DateTime.Now
            };

            return PartialView("NewSciencePostPartial", model);
        }
    }

}
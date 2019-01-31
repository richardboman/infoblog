using Infoblog.Models;
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
            var model = new EducationPostModel
            {
                Author = User.Identity.Name,
                Date = DateTime.Now
            };
            return PartialView("_NewPostPartial", model);
        }
    }
}
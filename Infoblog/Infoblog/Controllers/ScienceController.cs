using Infoblog.Models;
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
            var model = new ScienceModel
            {
                Author = User.Identity.Name,
                Date = DateTime.Now
            };

            return PartialView("NewSciencePostPartial", model);
        }
    }

}
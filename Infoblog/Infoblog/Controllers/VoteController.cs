using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        // GET: Vote
        public ActionResult ViewVote()
        {
            return View();
        }

        public ActionResult AddMeetingPost(string userId, int votes)
        {
            if (ModelState.IsValid)
            {
                var ctx = new ApplicationDbContext();
                ctx.VoteTable.Add(new VoteModel
                {
                    UserID = userId,
                    Votes = votes,
                });
                ctx.SaveChanges();

            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
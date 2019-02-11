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
            var ctx = new ApplicationDbContext();
            var meetings = ctx.MeetingPolls.OrderByDescending(p => p.Id).ToList();
            var meetingViewModels = new List<MeetingPoll>();
            foreach (var meeting in meetings)
            {
                //var user = ctx.Users.SingleOrDefault(u => u.Email == meeting.Author.ToString());
                meetingViewModels.Add(new MeetingPoll()
                {
                    Id = meeting.Id,
                    Author = meeting.Author,
                    Content = meeting.Content,
                    Title = meeting.Title,
                    PollOptions = meeting.PollOptions,
                    Participants = meeting.Participants
                });
                
            }
            return View(meetingViewModels);
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
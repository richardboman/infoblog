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
        public ActionResult ViewVote(int meetingPollId)
        {
            var ctx = new ApplicationDbContext();
            var meeting = ctx.MeetingPolls.Where(p => p.Id == meetingPollId).ToList().FirstOrDefault();
            var meetingViewModel = new MeetingPoll
                {
                    Id = meeting.Id,
                    Author = meeting.Author,
                    Content = meeting.Content,
                    Title = meeting.Title,
                    PollOptions = meeting.PollOptions,
                    Participants = meeting.Participants
                };

            return View(meetingViewModel);
        }

        /*public ActionResult AddVote(int pollId, int votes, string meetingTime, ApplicationUser a, string t, string c)
        {
            var ctx = new ApplicationDbContext();
            if (ModelState.IsValid)
            {

                ctx.MeetingPolls.Add(new MeetingPoll
                {
                    Author = a,
                    Title = t,
                    Content = c,
                    PollOptions = (new PollOption
                    {
                        Votes = votes,
                        MeetingPollId = pollId
                    }
                    )
                });
                    
                };
                ctx.SaveChanges();

            
            return Redirect(Request.UrlReferrer.ToString());
        }*/
    }
}
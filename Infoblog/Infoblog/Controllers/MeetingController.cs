using Infoblog.Models;
using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace Infoblog.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var ctx = new ApplicationDbContext();
            var user = ctx.Users.First(u => u.Id.Equals(userId));
            var mvm = new MeetingsViewModels();
            mvm.InvitedMeetingPolls = new List<MeetingPoll>();


            var pl = ctx.MeetingPolls.Select(m => new { Participants = m.Participants, MeetingPoll = m}).ToList();

            foreach(var p in pl)
            {
                if(p.Participants.Contains(user))
                {
                    mvm.InvitedMeetingPolls.Add(p.MeetingPoll);
                }
            }

            mvm.CreatedMeetingPolls = ctx.MeetingPolls.Where(m => m.Author.Id.Equals(userId)).ToList();
            
            return View(mvm);
        }

        public ActionResult Create()
        {
            var ctx = new ApplicationDbContext();
            var model = new CreateMeetingViewModel
            {
                MeetingTimes = new List<MeetingTime>(),
                AllUsers = ctx.Users.ToList()
            };
            return View(model);
        }

        [System.Web.Http.HttpPost]
        public ActionResult CreateMeeting([FromBody]MeetingData meetingData)
        {
            var ctx = new ApplicationDbContext();

            var userId = User.Identity.GetUserId();
            var author = ctx.Users.First(u => u.Id.Equals(userId));

            var mp = new MeetingPoll();
            mp.Title = meetingData.Title;
            mp.Content = meetingData.Content;
            mp.Author = author;
            mp.Participants = new List<ApplicationUser>();

            List<ApplicationUser> userList = new List<ApplicationUser>();
            foreach (var email in meetingData.Participants)
            {
                var user = ctx.Users.First(u => u.Email.Equals(email));
                mp.Participants.Add(user);
            }


            var pollOptions = new List<PollOption>();

            foreach(var time in meetingData.MeetingTimes)
            {
                var option = new PollOption() { MeetingTime = time, Votes = 0, MeetingPoll = mp };
                pollOptions.Add(option);
            }

            mp.PollOptions = pollOptions;

            ctx.MeetingPolls.Add(mp);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public class MeetingData
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string[] Participants { get; set; }
            public string[] MeetingTimes { get; set; }
        }
    }
}
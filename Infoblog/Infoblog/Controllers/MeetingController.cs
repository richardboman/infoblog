using Infoblog.Models;
using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Index()
        {
            return View();
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
            var mp = new MeetingPoll();
            mp.Title = meetingData.Title;
            mp.Content = meetingData.Content;

            var pollOptions = new List<PollOption>();

            foreach(var time in meetingData.MeetingTimes)
            {
                var option = new PollOption() { MeetingTime = time, Votes = 0, MeetingPoll = mp };
                pollOptions.Add(option);
            }

            mp.PollOptions = pollOptions;

            var ctx = new ApplicationDbContext();
            ctx.MeetingPolls.Add(mp);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public class MeetingData
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string[] MeetingTimes { get; set; }
        }
    }
}
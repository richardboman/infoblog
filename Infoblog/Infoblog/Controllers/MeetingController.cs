using Infoblog.Models;
using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Infoblog.Controllers
{
    [System.Web.Mvc.Authorize]
    public class MeetingController : Controller
    {

        public class MeetingData
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string[] Participants { get; set; }
            public string[] MeetingTimes { get; set; }
        }

        // GET: Meeting
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var ctx = new ApplicationDbContext();
            var user = ctx.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var mvm = new AllMeetingsViewModel();

            //Hämta de mötesomröstningar som den inloggade användaren är inbjuden till
            mvm.InvitedMeetingPolls = new List<MeetingPoll>();
            var pl = ctx.MeetingPolls.Select(m => new { m.Participants, MeetingPoll = m}).ToList();
            foreach(var p in pl)
            {
                if(p.Participants.Contains(user))
                {
                    mvm.InvitedMeetingPolls.Add(p.MeetingPoll);
                }
            }

            //Hämta till de möten som den inloggade användaren är inbjuden till
            mvm.InvitedMeetings = new List<Meeting>();
            var meetings = ctx.Meetings;
            var participants = meetings.Select(m => new { m, m.Participants }).ToList();
            foreach (var m in participants)
            {
               
                if (m.Participants.Contains(user))
                {
                    mvm.InvitedMeetings.Add(m.m);
                }
            }

            //Hämta användarens skapade möten
            mvm.CreatedMeetings = ctx.Meetings.Where(m => m.Author.Id.Equals(userId)).ToList();
            //Hämta användarens skapade mötesomröstningar
            mvm.CreatedMeetingPolls = ctx.MeetingPolls.Where(m => m.Author.Id.Equals(userId)).ToList();
            
            return View(mvm);
        }

        public ActionResult Create()
        {
            var ctx = new ApplicationDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentUser = ctx.Users.FirstOrDefault(u => u.Id == currentUserId);
            var allUsers = ctx.Users.ToList();
            allUsers.Remove(currentUser);
            var model = new CreateMeetingViewModel
            {
                MeetingTimes = new List<MeetingTime>(),
                AllUsers = allUsers
            };
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CreateMeetingPoll([FromBody]MeetingData meetingData)
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
            var notification = new SendEmailController();

            
            foreach(var time in meetingData.MeetingTimes)
            {
                var start = DateTime.Parse(time.Split(';')[0]);
                var end = DateTime.Parse(time.Split(';')[1]);

                var option = new PollOption() {
                    Start = start,
                    End = end,
                    Votes = 0,
                    MeetingPoll = mp
                };

                pollOptions.Add(option);
            }

            mp.PollOptions = pollOptions;

            ctx.MeetingPolls.Add(mp);
            ctx.SaveChanges();

            notification.EmailPollInvitation(mp);

            return RedirectToAction("Index");
        }

        public ActionResult PollResult(int id)
        {
            var ctx = new ApplicationDbContext();
            var mp = ctx.MeetingPolls.FirstOrDefault(m => m.Id == id);
            var view = new MeetingPollResultViewModel() {
                PollId = mp.Id,
                AuthorId = mp.Author.Id,
                Title = mp.Title,
                Content = mp.Content,
                Participants = mp.Participants,
                PollOptions = mp.PollOptions
                
            };
            return View(view);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CreateMeeting(MeetingPollResultViewModel model)
        {
            var ctx = new ApplicationDbContext();
            var poll = ctx.MeetingPolls.FirstOrDefault(p => p.Id == model.PollId);
            var start = DateTime.Parse(model.SelectedTime.Split(';')[0]);
            var end = DateTime.Parse(model.SelectedTime.Split(';')[1]);

            var meeting = new Meeting()
            {
                Title = poll.Title,
                Content = poll.Content,
                Author = poll.Author,
                Start = start,
                End = end,
                Participants = poll.Participants.ToList()
            };

            ctx.Meetings.Add(meeting);
            ctx.MeetingPolls.Remove(poll);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FullCalendar;
using Infoblog.Models;
using Microsoft.AspNet.Identity;

namespace Infoblog.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            var ctx = new ApplicationDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentUser = ctx.Users.FirstOrDefault(u => u.Id == currentUserId);
            var meetings = ctx.Meetings.Where(m => m.Participants.Select(p=> p.Id).ToList().Contains(currentUserId)).ToList();

            var events = new List<Event>();
            foreach (var m in meetings)
            {
                var additionalFields = new Dictionary<string, string>();
                additionalFields.Add("author", m.Author.FirstName + " " + m.Author.LastName);

                events.Add(new Event()
                {
                    Id = m.Id,
                    Title = m.Title,
                    AdditionalFields = additionalFields,
                    Start = m.Start,
                    End = m.End,
                    Url = "Meeting/ViewMeeting/" + m.Id
                   
                });
            }
            return View(events);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FullCalendar;
using Infoblog.Models;
using Microsoft.AspNet.Identity;

namespace Infoblog.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            var ctx = new ApplicationDbContext();
            var currentUserId = User.Identity.GetUserId();
            var invitedMeetings = ctx.Meetings.Where(m => m.Participants.Select(p=> p.Id).ToList().Contains(currentUserId)).ToList();
            var createdMeetings = ctx.Meetings.Where(m => m.Author.Id == currentUserId).ToList();
            var allMeetings = new List<Meeting>();
            allMeetings.AddRange(invitedMeetings);
            allMeetings.AddRange(createdMeetings);

            var events = new List<Event>();

            foreach (var m in allMeetings)
            {
                var additionalFields = new Dictionary<string, string>
                {
                    { "author", m.Author.FirstName + " " + m.Author.LastName },
                    { "content", m.Content }
                };
                var colorInCalendar = (m.Author.Id == currentUserId) ? Color.Red : Color.Blue;
  
                events.Add(new Event()
                {
                    Id = m.Id,
                    Title = m.Title,
                    AdditionalFields = additionalFields,
                    Start = m.Start,
                    End = m.End,
                    Url = Url.Action("View", "Meeting", new { id = m.Id }),
                    Color = colorInCalendar
                   
                });
            }
            return View(events);
        }
    }
}
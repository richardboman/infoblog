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

        public ActionResult AddNewTime()
        {
            var model = new MeetingTime();
            return PartialView("_AddTimePartial", model);
        }
    }
}
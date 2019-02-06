using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                MeetingTimes = new List<string>(),
                AllUsers = ctx.Users.ToList()
            };
            return View(model);
        }
    }
}
using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowNotifications (string reciverID)
        {

            var ctx = new ApplicationDbContext();
            ctx.Notifications.OrderByDescending(p => p.Date);
            
        }
    }
}
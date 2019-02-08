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
            return View();
        }
    }
}
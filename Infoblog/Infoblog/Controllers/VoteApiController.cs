using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    [System.Web.Http.RoutePrefix("api/controller")]
    public class VoteApiController : ApiController
    {

        [System.Web.Http.Route("vote/add")]
        [System.Web.Http.HttpGet]
        public void AddVotes(int pollId, int votes, int pollOptionId)
        {
            var ctx = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                var meeting = ctx.MeetingPolls.Where(p => pollId == p.Id).ToList().FirstOrDefault();

                var polloption = meeting.PollOptions.Where(p => p.Id == pollOptionId).ToList().FirstOrDefault();

                polloption.Votes = votes;

            };
            ctx.SaveChanges();
        }
    }
}


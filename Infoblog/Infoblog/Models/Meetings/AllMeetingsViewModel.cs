using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infoblog.Models.Meetings
{
    public class AllMeetingsViewModel
    {
        public List<MeetingPoll> CreatedMeetingPolls { get; set; }
        public List<MeetingPoll> InvitedMeetingPolls{ get; set; }
    }
}
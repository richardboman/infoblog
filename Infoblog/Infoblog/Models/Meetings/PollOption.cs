using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models.Meetings
{
    public class PollOption
    {
        [Key]
        public int Id { get; set; }
        public string MeetingTime { get; set; }
        public int Votes { get; set; }
        public int MeetingPollId { get; set; }
        public virtual MeetingPoll MeetingPoll { get; set; }
    }
}
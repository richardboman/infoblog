using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infoblog.Models.Meetings
{
    public class MeetingPollResultViewModel
    {
        public int PollId { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<PollOption> PollOptions { get; set; }
        public ICollection<ApplicationUser> Participants { get; set; }

        public string SelectedTime { get; set; }
    }
}
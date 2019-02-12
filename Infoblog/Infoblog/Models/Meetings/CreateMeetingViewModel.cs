using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class CreateMeetingViewModel
    {
        [DisplayName("Titel")]
        public string Title { get; set; }

        [DisplayName("Beskrivning")]
        public string Content { get; set; }

        public List<ApplicationUser> AllUsers { get; set; }
        public ICollection<ApplicationUser> Participants { get; set; }
        public List<MeetingTime> MeetingTimes { get; set; }
    }
}
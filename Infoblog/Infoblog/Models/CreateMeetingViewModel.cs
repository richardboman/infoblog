﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class CreateMeetingViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<ApplicationUser> AllUsers { get; set; }
        public ICollection<ApplicationUser> Participants { get; set; }
        public string MeetingTime { get; set; }
        public List<string> MeetingTimes { get; set; }
    }
}
using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class MeetingPoll
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string Content { get;  set; }
        public virtual ICollection<PollOption> PollOptions { get; set; }
        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}
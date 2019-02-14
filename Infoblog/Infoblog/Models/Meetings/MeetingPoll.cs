using Infoblog.Models.Meetings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Skapad av:")]
        public virtual ApplicationUser Author { get; set; }
        [DisplayName("Titel")]
        public string Title { get; set; }
        [DisplayName("Beskrivning")]
        public string Content { get;  set; }
        public virtual ICollection<PollOption> PollOptions { get; set; }
        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}
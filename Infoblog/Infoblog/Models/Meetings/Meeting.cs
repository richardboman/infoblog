using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class Meeting
    {
        public Meeting()
        {
            Participants = new HashSet<ApplicationUser>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MeetingTime { get; set; }
        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}
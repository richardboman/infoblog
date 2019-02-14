using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class Meeting
    {
    
        [Key]
        public int Id { get; set; }

        [DisplayName("Titel")]
        public string Title { get; set; }

        [DisplayName("Beskrivning")]
        public string Content { get; set; }

        [DisplayName("Start")]
        public DateTime Start { get; set; }

        [DisplayName("Slut")]
        public DateTime End { get; set; }

        [DisplayName("Skapad av")]
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}
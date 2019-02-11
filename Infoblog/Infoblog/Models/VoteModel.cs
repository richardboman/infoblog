using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class VoteModel
    {
        [Key]
        public string ID { get; set; }

        public int Votes { get; set; }
        public string UserID { get; set; }
        

    }
}
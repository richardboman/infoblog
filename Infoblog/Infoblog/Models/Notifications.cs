using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }
        
        public string ReciverID { get; set; }
        public string Message { get; set; }

    }
}
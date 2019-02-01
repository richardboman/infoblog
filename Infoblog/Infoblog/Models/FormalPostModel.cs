﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class FormalPostModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Rubrik")]
        public string Title { get; set; }
        [Display(Name = "Meddelande")]
        public string Content { get; set; }
        [Display(Name = "Avsändare")]
        public string Author { get; set; }
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        public string FilePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

    }
}
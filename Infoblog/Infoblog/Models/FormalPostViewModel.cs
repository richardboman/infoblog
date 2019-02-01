﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class FormalPostViewModel
    {
        public List<FormalPostModel> Posts { get; set; }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [Display(Name = "Meddelande")]
        public string Content { get; set; }

        [Display(Name = "Avsändare")]
        public string Author { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
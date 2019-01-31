using System;
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
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
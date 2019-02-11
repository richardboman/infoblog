using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class InformalPostViewModel
    {
        public List<InformalPostModel> InformalPosts { get; set; }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rubrik")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Rubrik måste innehålla mellan 3 och 100 tecken")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Meddelande")]
        public string Content { get; set; }

        [Display(Name = "Avsändare")]
        public string Author { get; set; }

        public int CategoryId { get; set; }

        public string FilePath { get; set; }

        [ValidateFile(ErrorMessage = "Fel filformat. Välj, png, jpg eller gif")]
        public HttpPostedFileBase File { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Rubrik")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Rubrik måste innehålla mellan 3 och 100 tecken")]
        public string Title { get; set; }

        [Display(Name = "Meddelande")]
        [Required]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Meddelande måste vara minst 3 tecken och maximalt 2000 tecken.")]
        public string Content { get; set; }

        [Display(Name = "Avsändare")]
        [Required]
        public string Author { get; set; }

        [Display(Name = "Datum")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Förnamn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required]
        public string LastName { get; set; }

        public string FilePath { get; set; }

        [ValidateFile(ErrorMessage = "Fel filformat. Välj, png, jpg, gif, doc, docx, pdf")]
        public HttpPostedFileBase File { get; set; }
    }
}
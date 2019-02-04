using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Infoblog.Models
{
    public class ValidateFile: RequiredAttribute
    {

        public override bool IsValid (object value)

        {
            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                var allowedExtensions = new[] { ".pdf", ".jpg",".png", ".doc","docx",".jpeg"};
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(checkextension))
                {
                    return false;
                }

                else
                {
                    return true;
                
                }

            }
            else
            {
                return true;
            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infoblog.Models
{
    public class NotificationSettingsViewModel
    {
        public List<CategoryModel> Categories { get; set; }
        public List<CategoryModel> MyCategoryInNotifications { get; set; }
    }
}
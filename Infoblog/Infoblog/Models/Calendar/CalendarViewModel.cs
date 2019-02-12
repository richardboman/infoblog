using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infoblog.Models.Calendar
{
    public class CalendarViewModel
    {
        public List<FullCalendar.Event> Events { get; set; }
    }
}
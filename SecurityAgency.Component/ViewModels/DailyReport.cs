using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityAgency.Common.ViewModels
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public string SecurityGuard { get; set; }
        public string Customer { get; set; }
        public string Hours { get; set; }
    }
}
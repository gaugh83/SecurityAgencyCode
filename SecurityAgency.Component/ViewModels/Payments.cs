using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityAgency.Common.ViewModels
{
    public class Payments
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public long ChequeNo { get; set; }
        public int InvoiceNo { get; set; }
        public string Description { get; set; }
    }
}
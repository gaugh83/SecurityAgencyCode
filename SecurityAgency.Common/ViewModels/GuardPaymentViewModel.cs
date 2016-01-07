using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
    public class GuardPaymentViewModel
    {
        public int GuardPaymentId { get; set; }

        [Required]
        public int GuardId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PaymentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EndDate { get; set; }
                             
        [Required(ErrorMessage = "Please fill Amount")]
        public decimal Amount { get; set; }

        [StringLength(500, ErrorMessage = "Comments max length is 500 characters")]
        public string Comments { get; set; }

        public string GuardName { get; set; }

        public string Status { get; set; }

        public decimal HourlyRate { get; set; }

        public int TotalHours { get; set; }

        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public SelectList GuardList { get; set; }
        //For Reports
        public Nullable<int> PaidHours { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<int> PendingHours { get; set; }
        public Nullable<decimal> PendingAmount { get; set; }
    }
}

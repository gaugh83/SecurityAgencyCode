using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
    public class CustomerPaymentViewModel
    {
        public int CustomerPaymentId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int InvoiceNo { get; set; }

        [Required(ErrorMessage = "Please fill Begin Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "Please fill End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please fill Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Please fill Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [Required(ErrorMessage = "Please fill Total Hours")]
        public int TotalHours { get; set; }

        public DateTime currentDate = DateTime.Now;
             
        [Required(ErrorMessage = "Please fill Amount")]
        public decimal Amount { get; set; }

        [StringLength(500, ErrorMessage = "Comments max length is 500 characters")]
        public string Comments { get; set; }

        public string CustomerName { get; set; }

        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public SelectList CustomerList { get; set; }

        public SelectList InvoiceList { get; set; }

    }
}

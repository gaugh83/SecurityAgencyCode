using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
    public class CustomerInvoiceViewModel: CustomerViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime currentDate = DateTime.Now;

        [Required(ErrorMessage = "Please select Payment Type")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "Please fill Invoice Number")]
        public int InvoiceNo { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please fill Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime InvoiceDate
        {
            get
            {
                return currentDate;
            }

            set
            {
                currentDate = value;
            }
        }

        [Required(ErrorMessage = "Please fill Begin Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "Please fill End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please fill Total Hours")]
        public int TotalHours { get; set; }

        [Required(ErrorMessage = "Please fill Hourly Rate")]
        public Nullable<decimal> HourlyRate { get; set; }

        [Required(ErrorMessage = "Please fill Amount")]
        public int Amount { get; set; }

        [StringLength(500, ErrorMessage = "Description max length is 500 characters")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "InternalComments max length is 500 characters")]
        public string InternalComments { get; set; }

        public string CustomerName { get; set; }

        public Nullable<int> CustomerPaymentId { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }

        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public SelectList CustomerList { get; set; }
    }

}

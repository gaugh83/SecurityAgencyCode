using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
   public class DailyLogViewModel
    {
       public DateTime currentDate=DateTime.Now;

       public int DailyLogId { get; set; }
       [Required]
       [DataType(DataType.Date)]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       public DateTime Dated
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

      [Required(ErrorMessage="Please Enter Hours")]
        public int Hours { get; set; }
       [Required]
      public string CustomerName { get; set; }
       [Required]
       public string GuardName { get; set; }
       [Required(ErrorMessage="Please enter comments")]
        public string Comments { get; set; }
       [Required]
        public int GuardId { get; set; }
       [Required]
        public int CustomerId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public SelectList CustomerList { get; set; }
        public SelectList GuardList { get; set; }
    }
}

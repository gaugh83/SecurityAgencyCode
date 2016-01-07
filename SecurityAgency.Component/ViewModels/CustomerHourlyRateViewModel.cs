using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SecurityAgency.Common.ViewModels
{
    public class CustomerHourlyRateViewModel
    {
        DateTime currentDate=DateTime.Now;
        public int HourlyRateId { get; set; }
        [Required(ErrorMessage="Please Enter the hourly rate ")]
        public decimal HourlyRate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
  public System.DateTime EffectiveFrom
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
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EffectiveTo
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
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string Name { get; set; }

        public SelectList CustomerList { get; set; }
    
    }
}

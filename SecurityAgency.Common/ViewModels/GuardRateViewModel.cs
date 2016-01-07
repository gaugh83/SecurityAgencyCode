using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
   public class GuardRateViewModel
    {
       DateTime currentDate = DateTime.Now;
        public int HourlyRateId { get; set; }
       [Required(ErrorMessage="Please Enter Hourly Rate")]
        public decimal HourlyRate { get; set; }
        public int GuardId { get; set; }
        [DataType(DataType.Date)]
       [Required]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveFrom
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
        [DataType(DataType.Date)]
       [Required]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveTo
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
        public SelectList GuardList { get; set; }
    }
}

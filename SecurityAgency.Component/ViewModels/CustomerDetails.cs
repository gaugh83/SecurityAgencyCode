using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityAgency.Common.ViewModels
{
    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage="Please enter Address")]
        [StringLength(50, ErrorMessage ="Address max length is 50 characters")]
public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Number")]
        [StringLength(10, ErrorMessage = "The Contact Number must contains 10 characters")] 
        public long ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
       [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",ErrorMessage = "Please Enter Valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage="Please Enter Zip")]
        [StringLength(6,ErrorMessage="Zip must contain 6 characters")]
        public int Zip { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Last_ModifiedBy { get; set; }
        public DateTime Last_ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Active { get; set; }
        public int DeletedBy { get; set; }
       
 
     

    }
}
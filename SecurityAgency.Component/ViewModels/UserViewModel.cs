using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;



namespace SecurityAgency.Common.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string UserName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Please enter password!")]
        //[StringLength(15, ErrorMessage = "The password must be at least 6 characters long", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        [Required(ErrorMessage = "Selection is a MUST")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter mobile number.")]
        [StringLength(10, ErrorMessage = "The mobile no. must contain 10 digits", MinimumLength = 10)]
        [Display(Name = "Mobile")]
        public string ContactNo { get; set; }

        
        public SelectList Rolelist { get; set; }

        public string Zip { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Last_ModifiedBy { get; set; }
        public DateTime Last_ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Active { get; set; }
        public int DeletedBy { get; set; }

        public string Password { get; set; }



    }
}
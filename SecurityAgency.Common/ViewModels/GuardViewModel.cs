﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
    public class GuardViewModel
    {
        public int GuardId { get; set; }
        [Required(ErrorMessage="Please Enter Name")]
        public string Name { get; set; }
        [StringLength(512, ErrorMessage = "Address max length is 512 characters")]
        public string Address { get; set; }
        [StringLength(20, ErrorMessage = "The Contact Number must contains 10 characters")] 
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter SSN")]
        public string SSN { get; set; }
        [StringLength(6, ErrorMessage = "Zip must contain 6 characters")]
        public string Zip { get; set; }
        public decimal HourlyRate { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public SelectList Guardlist { get; set; }
        public string NameSSN { get; set; }
    }
}

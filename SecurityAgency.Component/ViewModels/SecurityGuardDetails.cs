using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityAgency.Common.ViewModels
{
    public class SecurityGuardDetails
    {
        public int GuardId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long ContactNo { get; set; }
        public string Email { get; set; }
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
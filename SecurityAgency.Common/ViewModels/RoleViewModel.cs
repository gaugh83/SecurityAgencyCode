using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecurityAgency.Common.ViewModels
{
   public class RoleViewModel
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Role1 { get; set; }

        public SelectList Rolelist { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Last_ModifiedBy { get; set; }
        public DateTime Last_ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Active { get; set; }
        public int DeletedBy { get; set; }
    }
}

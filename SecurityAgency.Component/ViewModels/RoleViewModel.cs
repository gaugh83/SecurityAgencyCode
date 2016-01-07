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
        public string Role1 { get; set; }

        public SelectList Rolelist { get; set; }
    }
}

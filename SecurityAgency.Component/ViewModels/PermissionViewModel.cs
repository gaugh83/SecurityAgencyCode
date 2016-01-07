using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.ViewModels
{
   public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string Role { get; set; }
        public string ModuleName { get; set; }
        public string Name { get; set; }
    }
}

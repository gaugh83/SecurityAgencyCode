using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Models
{
    public class ActiveUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
    }
}

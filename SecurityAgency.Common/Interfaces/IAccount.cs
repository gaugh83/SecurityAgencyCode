using SecurityAgency.Common.Models;
using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface IAccount
    {
        ActiveUser CheckLogin(UserViewModel userModel);
    }
}

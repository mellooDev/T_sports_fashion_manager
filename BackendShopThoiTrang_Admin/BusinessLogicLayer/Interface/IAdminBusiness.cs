using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface IAdminBusiness
    {
        AdminsModel Login(string username, string password);
    }
}

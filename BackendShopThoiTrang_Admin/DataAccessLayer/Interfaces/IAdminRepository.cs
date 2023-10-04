using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IAdminRepository
    {
        AdminsModel Login(string username, string password);

        //AdminsModel GetAdminbyId(string id);

        //bool Create(AdminsModel model);

        //bool Update(AdminsModel model);

        //bool Delete(string Id);

    }
}

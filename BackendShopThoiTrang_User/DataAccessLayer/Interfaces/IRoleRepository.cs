using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IRoleRepository
    {
        bool Create(RolesModel model);

        bool Update(RolesModel model);

        bool Delete(string id);

    }
}

using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface ICategoryBusiness
    {

        bool Create(CategoryMainModel model);

        bool Update(CategoryMainModel model);

        bool Delete(string id);
    }
}

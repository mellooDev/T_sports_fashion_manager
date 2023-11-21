using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface ICategoryRepository
    {

        bool Create(CategoryMainModel model);

        bool Update(CategoryMainModel model);

        bool Delete(string Id);

    }
}

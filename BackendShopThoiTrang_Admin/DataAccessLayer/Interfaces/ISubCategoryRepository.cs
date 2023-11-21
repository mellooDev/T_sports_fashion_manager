using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface ISubCategoryRepository
    {
        SubCategoriesModel GetCategorybyID(string id);

        SubCategoriesModel GetProductByCategoryName(string name);
   

    }
}

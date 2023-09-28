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
        CategoriesModel GetCategorybyID(string id);

        List<CategoriesModel> GetAllCategories();

        bool Create(CategoriesModel model);

        bool Update(CategoriesModel model);

        bool Delete(string id);
    }
}

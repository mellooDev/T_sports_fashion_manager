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
        SubCategoriesModel GetCategorybyID(string id);

        SubCategoriesModel GetProductByCategoryName(string name);

        List<SubCategoriesModel> GetAllCategories();

        bool Create(SubCategoriesModel model);

        bool Update(SubCategoriesModel model);

        bool Delete(string id);
    }
}

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
        SubCategoriesModel GetCategorybyID(string id);

        SubCategoriesModel GetProductByCategoryName(string name);

        List<SubCategoriesModel> GetAllCategories();

        bool Create(SubCategoriesModel model);

        bool Update(SubCategoriesModel model);

        bool Delete(string Id);

    }
}

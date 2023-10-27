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
        CategoriesModel GetCategorybyID(string id);

        CategoriesModel GetProductByCategoryName(string name);

        List<CategoriesModel> GetAllCategories();

        bool Create(CategoriesModel model);

        bool Update(CategoriesModel model);

        bool Delete(string Id);

    }
}

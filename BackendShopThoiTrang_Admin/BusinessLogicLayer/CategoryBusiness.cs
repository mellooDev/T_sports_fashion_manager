using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private ICategoryRepository _res;
        public CategoryBusiness(ICategoryRepository res)
        {
            _res = res;
        }
        public CategoriesModel GetCategorybyID(string id)
        {
            return _res.GetCategorybyID(id);
        }

        public CategoriesModel GetProductByCategoryName(string name)
        {
            return _res.GetProductByCategoryName(name);
        }

        public List<CategoriesModel> GetAllCategories()
        {
            return _res.GetAllCategories();
        }

        public bool Create(CategoriesModel model)
        {
            return _res.Create(model);
        }

        public bool Update(CategoriesModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
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
        public SubCategoriesModel GetCategorybyID(string id)
        {
            return _res.GetCategorybyID(id);
        }

        public SubCategoriesModel GetProductByCategoryName(string name)
        {
            return _res.GetProductByCategoryName(name);
        }

        public List<SubCategoriesModel> GetAllCategories()
        {
            return _res.GetAllCategories();
        }

        public bool Create(SubCategoriesModel model)
        {
            return _res.Create(model);
        }

        public bool Update(SubCategoriesModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
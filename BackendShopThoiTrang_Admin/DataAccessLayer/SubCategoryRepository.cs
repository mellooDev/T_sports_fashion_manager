using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;

namespace DataAccessLayer
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private IDatabaseHelper _dbHelper;
        public SubCategoryRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public SubCategoriesModel GetCategorybyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_category_by_id",
                     "@category_id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SubCategoriesModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubCategoriesModel GetProductByCategoryName(string name)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_product_by_cate",
                     "@category_name", name);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SubCategoriesModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}

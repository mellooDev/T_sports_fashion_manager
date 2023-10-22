using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;
using System.Reflection;

namespace DataAccessLayer
{
    public class ProductRepository : IProductRepository
    {
        private IDatabaseHelper _dbHelper;
        public ProductRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public ProductsModel GetProductbyId(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_product_by_id",
                     "@product_id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductsModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductsModel> GetAllProducts()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_all_product");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductsModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create(ProductsModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_product",
                    "@product_name", model.product_name,
                    "@description", model.description,
                    "@price", model.price,
                    "@discount", model.discount,
                    "@image_link", model.image_link,
                    "@product_quantity", model.product_quantity,
                    "@updated_date", model.updated_date,
                    "@category_id", model.category_id,
                    "@brand_id", model.brand_id);
                if ((result != null && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ProductsModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_product",
                    "@product_id", model.product_id,
                    "@product_name", model.product_name,
                    "@price", model.price,
                    "@discount", model.discount,
                    "@image_link", model.image_link,
                    "@description", model.description,
                    "@product_quantity", model.product_quantity,
                    "@created_date", model.created_date,
                    "@updated_date", model.updated_date,
                    "@category_id", model.category_id,
                    "@brand_id", model.brand_id);
                if ((result != null && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_product",
                "@product_id", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;
using System.Net;
using System.Reflection;

namespace DataAccessLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private IDatabaseHelper _dbHelper;
        public CustomerRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public CustomersModel GetCustomerbyId(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_brand_by_id",
                     "@Id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CustomersModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomersModel> GetAllCustomer()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_all_brand");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CustomersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create(CustomersModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_customer",
                    "@username", model.username,
                    "@password", model.password,
                    "@customer_name", model.customer_name,
                    "@phone_number", model.phone_number,
                    "@address", model.address,
                    "@email", model.email,
                    "@role_id", model.role_id);
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

        public bool Update(CustomersModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_customer",
                    "@customer_id", model.customer_id,
                    "@username", model.username,
                    "@password", model.password,
                    "@customer_name", model.customer_name,
                    "@phone_number", model.phone_number,
                    "@address", model.address,
                    "@email", model.email,
                    "@role_id", model.role_id);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_customer",
                "@customer_id", id);
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

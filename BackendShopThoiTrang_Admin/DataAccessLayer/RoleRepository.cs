using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;

namespace DataAccessLayer
{
    public class RoleRepository : IRoleRepository
    {
        private IDatabaseHelper _dbHelper;
        public RoleRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        public bool Create(RolesModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_role",
                    "@role_name", model.role_name);
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

        public bool Update(RolesModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_role",
                    "@role_id", model.role_id,
                    "@role_name", model.role_name);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_brand",
                "@brand_id", id);
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

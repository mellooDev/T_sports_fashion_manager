using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;
using System.Reflection;

namespace DataAccessLayer
{
    public class ShippingDetailsRepository : IShippingDetailsRepository
    {
        private IDatabaseHelper _dbHelper;
        public ShippingDetailsRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public Shipping_detailsModel GetShippingbyId(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_shipping_detail_by_id",
                     "@detail_id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Shipping_detailsModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create(Shipping_detailsModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_shipping_detail",
                    "@consignee_name", model.consignee_name,
                    "@delivery_address", model.delivery_address,
                    "@phone_number", model.phone_number,
                    "@shipping_note", model.shipping_note,
                    "@shipping_method", model.shipping_method);
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

        public bool Update(Shipping_detailsModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_shipping_detail",
                    "@detail_id", model.detail_id,
                    "@consignee_name", model.consignee_name,
                    "@delivery_address", model.delivery_address,
                    "@phone_number", model.phone_number,
                    "@shipping_note", model.shipping_note,
                    "@shipping_method", model.shipping_method);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_shipping_detail",
                "@detail_id", id);
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

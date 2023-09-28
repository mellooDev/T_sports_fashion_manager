using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;

namespace DataAccessLayer
{
    public class ImportRepository : IImportRepository
    {
        private IDatabaseHelper _dbHelper;
        public ImportRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        //public ImportModel GetImportbyId(string id)
        //{
        //    string msgError = "";
        //    try
        //    {
        //        var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_brand_by_id",
        //             "@Id", id);
        //        if (!string.IsNullOrEmpty(msgError))
        //            throw new Exception(msgError);
        //        return dt.ConvertTo<ImportModel>().FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public bool Create(ImportModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_import_create",
                "@category_id", model.category_id,
                "@total_money", model.total_money,
                "@list_json_import_details", model.list_json_import_details != null ? MessageConvert.SerializeObject(model.list_json_import_details) : null);
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

        //public bool Update(BrandsModel model)
        //{
        //    string msgError = "";
        //    try
        //    {
        //        var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_brand",
        //            "@brand_id", model.brand_id,
        //            "@brand_name", model.brand_name);
        //        if ((result != null && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
        //        {
        //            throw new Exception(Convert.ToString(result) + msgError);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

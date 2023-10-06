//using DataAccessLayer.Helper;
//using DataAccessLayer.Helper.Interfaces;
//using DataModel;

//namespace DataAccessLayer
//{
//    public class LocationRepository : ILocationRepository
//    {
//        private IDatabaseHelper _dbHelper;
//        public LocationRepository(IDatabaseHelper dbHelper)
//        {
//            _dbHelper = dbHelper;
//        }
//        public bool Create(LocationModel model)
//        {
//            string msgError = "";
//            try
//            {
//                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_brand",
//                    "@brand_name", model.brand_name);
//                if ((result != null && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
//                {
//                    throw new Exception(Convert.ToString(result) + msgError);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        //public bool Update(BrandsModel model)
//        //{
//        //    string msgError = "";
//        //    try
//        //    {
//        //        var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_brand",
//        //            "@brand_id", model.brand_id,
//        //            "@brand_name", model.brand_name);
//        //        if ((result != null && string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
//        //        {
//        //            throw new Exception(Convert.ToString(result) + msgError);
//        //        }
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}

//        //public bool Delete(string id)
//        //{
//        //    string msgError = "";
//        //    try
//        //    {
//        //        var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_brand",
//        //        "@brand_id", id);
//        //        if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
//        //        {
//        //            throw new Exception(Convert.ToString(result) + msgError);
//        //        }
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
//    }
//}

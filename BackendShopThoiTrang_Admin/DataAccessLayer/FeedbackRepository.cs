using DataAccessLayer.Helper;
using DataAccessLayer.Helper.Interfaces;
using DataModel;
using System.Reflection;

namespace DataAccessLayer
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private IDatabaseHelper _dbHelper;
        public FeedbackRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public FeedbacksModel GetFeedbackbyId(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_feedback_by_id",
                     "@brand_id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<FeedbacksModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create(FeedbacksModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_feedback",
                    "@first_name", model.first_name,
                    "@last_name", model.last_name,
                    "@email", model.email,
                    "@phone_number", model.phone_number,
                    "@subject_name", model.subject_name,
                    "@feedback_content", model.feedback_content);
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

        public bool Update(FeedbacksModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_update_feedback",
                    "@feedback_id", model.feedback_id,
                    "@first_name", model.first_name,
                    "@last_name", model.last_name,
                    "@email", model.email,
                    "@phone_number", model.phone_number,
                    "@subject_name", model.subject_name,
                    "@feedback_content", model.feedback_content);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_delete_feedback",
                "@feedback_id", id);
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

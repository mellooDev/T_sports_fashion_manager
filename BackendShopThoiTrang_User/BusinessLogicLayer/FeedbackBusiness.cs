using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        private IFeedbackRepository _res;
        public FeedbackBusiness(IFeedbackRepository res)
        {
            _res = res;
        }
        public FeedbacksModel GetFeedbackbyId(string id)
        {
            return _res.GetFeedbackbyId(id);
        }

        public bool Create(FeedbacksModel model)
        {
            return _res.Create(model);
        }

        public bool Update(FeedbacksModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
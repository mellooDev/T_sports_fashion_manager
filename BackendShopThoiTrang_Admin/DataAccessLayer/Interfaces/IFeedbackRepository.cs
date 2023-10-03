using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IFeedbackRepository
    {
        FeedbacksModel GetFeedbackbyId(string id);

        bool Create(FeedbacksModel model);

        bool Update(FeedbacksModel model);

        bool Delete(string Id);

    }
}

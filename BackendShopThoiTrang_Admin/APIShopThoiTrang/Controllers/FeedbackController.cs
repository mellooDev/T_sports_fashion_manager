using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackBusiness _feedbackBusiness;
        public FeedbackController(IFeedbackBusiness feedbackBusiness) 
        {
            _feedbackBusiness = feedbackBusiness;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public FeedbacksModel GetFeedbackbyId(string id)
        {
            return _feedbackBusiness.GetFeedbackbyId(id);
        }


        [Route("create-feedback")]
        [HttpPost]
        public FeedbacksModel CreateFeedback([FromBody] FeedbacksModel model)
        {
            _feedbackBusiness.Create(model);
            return model;
        }

        [Route("update-feedback")]
        [HttpPost]
        public FeedbacksModel UpdateFeedback([FromBody] FeedbacksModel model)
        {
            _feedbackBusiness.Update(model);
            return model;
        }

        [Route("delete-feedback")]
        [HttpPost]
        public IActionResult DeleteFeedback([FromBody] Dictionary<string, object> formData)
        {
            string feedback_id = "";
            if (formData.Keys.Contains("feedback_id") && !string.IsNullOrEmpty(Convert.ToString(formData["feedback_id"]))) { feedback_id = Convert.ToString(formData["feedback_id"]); }
            _feedbackBusiness.Delete(feedback_id);
            return Ok();
        }




        //[Route("search")]
        //[HttpPost]
        //public IActionResult Search([FromBody] Dictionary<string, object> formData)
        //{
        //    var response = new ResponseModel();
        //    try
        //    {
        //        var page = int.Parse(formData["page"].ToString());
        //        var pageSize = int.Parse(formData["pageSize"].ToString());
        //        string ten_khach = "";
        //        if (formData.Keys.Contains("ten_khach") && !string.IsNullOrEmpty(Convert.ToString(formData["ten_khach"]))) { ten_khach = Convert.ToString(formData["ten_khach"]); }
        //        string diachi = "";
        //        if (formData.Keys.Contains("diachi") && !string.IsNullOrEmpty(Convert.ToString(formData["diachi"]))) { diachi = Convert.ToString(formData["diachi"]); }
        //        long total = 0;
        //        var data = _khachBusiness.Search(page, pageSize, out total, ten_khach, diachi);
        //        return Ok(
        //           new
        //           {
        //               TotalItems = total,
        //               Data = data,
        //               Page = page,
        //               PageSize = pageSize
        //           }
        //           );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}

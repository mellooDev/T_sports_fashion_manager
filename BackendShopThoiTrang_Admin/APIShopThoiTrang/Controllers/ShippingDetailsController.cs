using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingDetailsController : ControllerBase
    {
        private IShippingDetailsBusiness _shippingDetailsBusiness;
        public ShippingDetailsController(IShippingDetailsBusiness shippingDetailsBusiness) 
        {
            _shippingDetailsBusiness = shippingDetailsBusiness;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public Shipping_detailsModel GetShippingbyId(string id)
        {
            return _shippingDetailsBusiness.GetShippingbyId(id);
        }

        [Route("create-shippingDetails")]
        [HttpPost]
        public Shipping_detailsModel CreateShipping([FromBody] Shipping_detailsModel model)
        {
            _shippingDetailsBusiness.Create(model);
            return model;
        }

        [Route("update-shippingDetails")]
        [HttpPost]
        public Shipping_detailsModel UpdateShipping([FromBody] Shipping_detailsModel model)
        {
            _shippingDetailsBusiness.Update(model);
            return model;
        }

        [Route("delete-shippingDetails")]
        [HttpPost]
        public IActionResult DeleteShipping([FromBody] Dictionary<string, object> formData)
        {
            string detail_id = "";
            if (formData.Keys.Contains("detail_id") && !string.IsNullOrEmpty(Convert.ToString(formData["detail_id"]))) { detail_id = Convert.ToString(formData["detail_id"]); }
            _shippingDetailsBusiness.Delete(detail_id);
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

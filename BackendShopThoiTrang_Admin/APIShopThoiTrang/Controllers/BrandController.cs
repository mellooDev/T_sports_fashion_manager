using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandBusiness _brandBusiness;
        public BrandController(IBrandBusiness brandBusiness) 
        {
            _brandBusiness = brandBusiness;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public BrandsModel GetBrandbyId(string id)
        {
            return _brandBusiness.GetBrandbyId(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<BrandsModel> GetAllBrands()
        {
            return _brandBusiness.GetAllBrands();
        }


        [Route("create-brand")]
        [HttpPost]
        public BrandsModel CreateBrand([FromBody] BrandsModel model)
        {
            _brandBusiness.Create(model);
            return model;
        }

        [Route("update-khach")]
        [HttpPost]
        public BrandsModel UpdateBrand([FromBody] BrandsModel model)
        {
            _brandBusiness.Update(model);
            return model;
        }

        [Route("delete-Khach")]
        [HttpPost]
        public IActionResult DeleteBrand([FromBody] Dictionary<string, object> formData)
        {
            string KhachHangID = "";
            if (formData.Keys.Contains("brand_id") && !string.IsNullOrEmpty(Convert.ToString(formData["brand_id"]))) { KhachHangID = Convert.ToString(formData["brand_id"]); }
            _brandBusiness.Delete(KhachHangID);
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

using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _categoryBusiness;
        public CategoryController(ICategoryBusiness categoryBusiness) 
        {
            _categoryBusiness = categoryBusiness;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public SubCategoriesModel GetCategorybyID(string id)
        {
            return _categoryBusiness.GetCategorybyID(id);
        }

        [Route("get-product-by-cate-name/{name}")]
        [HttpGet]
        public SubCategoriesModel GetProductByCategoryName(string name)
        {
            return _categoryBusiness.GetProductByCategoryName(name);
        }

        [AllowAnonymous]
        [Route("get-all")]
        [HttpGet]
        public List<SubCategoriesModel> GetAllCategories()
        {
            return _categoryBusiness.GetAllCategories();
        }


        [Route("create-category")]
        [HttpPost]
        public SubCategoriesModel CreateKhach([FromBody] SubCategoriesModel model)
        {
            _categoryBusiness.Create(model);
            return model;
        }

        [Route("update-category")]
        [HttpPut]
        public SubCategoriesModel UpdateKhach([FromBody] SubCategoriesModel model)
        {
            _categoryBusiness.Update(model);
            return model;
        }

        [Route("delete-category")]
        [HttpDelete]
        public IActionResult DeleteKhach([FromBody] Dictionary<string, object> formData)
        {
            string KhachHangID = "";
            if (formData.Keys.Contains("KhachHangID") && !string.IsNullOrEmpty(Convert.ToString(formData["KhachHangID"]))) { KhachHangID = Convert.ToString(formData["KhachHangID"]); }
            _categoryBusiness.Delete(KhachHangID);
            return Ok();
        }
    }
}

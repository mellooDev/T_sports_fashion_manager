using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
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
        public CategoriesModel GetCategorybyID(string id)
        {
            return _categoryBusiness.GetCategorybyID(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<CategoriesModel> GetAllCategories()
        {
            return _categoryBusiness.GetAllCategories();
        }


        [Route("create-category")]
        [HttpPost]
        public CategoriesModel CreateKhach([FromBody] CategoriesModel model)
        {
            _categoryBusiness.Create(model);
            return model;
        }

        [Route("update-category")]
        [HttpPost]
        public CategoriesModel UpdateKhach([FromBody] CategoriesModel model)
        {
            _categoryBusiness.Update(model);
            return model;
        }

        [Route("delete-category")]
        [HttpPost]
        public IActionResult DeleteKhach([FromBody] Dictionary<string, object> formData)
        {
            string KhachHangID = "";
            if (formData.Keys.Contains("KhachHangID") && !string.IsNullOrEmpty(Convert.ToString(formData["KhachHangID"]))) { KhachHangID = Convert.ToString(formData["KhachHangID"]); }
            _categoryBusiness.Delete(KhachHangID);
            return Ok();
        }
    }
}

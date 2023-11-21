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


        [Route("create-category")]
        [HttpPost]
        public CategoryMainModel CreateCategory([FromBody] CategoryMainModel model)
        {
            _categoryBusiness.Create(model);
            return model;
        }

        [Route("update-category")]
        [HttpPut]
        public CategoryMainModel UpdateCategory([FromBody] CategoryMainModel model)
        {
            _categoryBusiness.Update(model);
            return model;
        }

        [Route("delete-category")]
        [HttpDelete]
        public IActionResult DeleteCategory([FromBody] Dictionary<string, object> formData)
        {
            string categoryMain_id = "";
            if (formData.Keys.Contains("categoryMain_id") && !string.IsNullOrEmpty(Convert.ToString(formData["categoryMain_id"]))) { categoryMain_id = Convert.ToString(formData["categoryMain_id"]); }
            _categoryBusiness.Delete(categoryMain_id);
            return Ok();
        }
    }
}

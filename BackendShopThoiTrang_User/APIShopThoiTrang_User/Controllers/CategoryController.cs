using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Authorize (Roles = "2")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _categoryBusiness;
        public CategoryController(ICategoryBusiness categoryBusiness) 
        {
            _categoryBusiness = categoryBusiness;
        }


        [Route("get-product-by-cate-name/{name}")]
        [HttpGet]
        public SubCategoriesModel GetProductByCategoryName(string name)
        {
            return _categoryBusiness.GetProductByCategoryName(name);
        }

        [Route("get-all")]
        [HttpGet]
        public List<SubCategoriesModel> GetAllCategories()
        {
            return _categoryBusiness.GetAllCategories();
        }


    }
}

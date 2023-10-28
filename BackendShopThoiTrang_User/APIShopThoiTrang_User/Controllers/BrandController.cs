using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "2")]

        [Route("get-prod-by-name/{name}")]
        [HttpGet]
        public BrandsModel GetProductbyBrandName(string name)
        {
            return _brandBusiness.GetProductbyBrandName(name);
        }

        [Route("get-all")]
        [HttpGet]
        public List<BrandsModel> GetAllBrands()
        {
            return _brandBusiness.GetAllBrands();
        }

    }
}

using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Authorize(Roles = "2")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBusiness;
        private string _path;
        private IWebHostEnvironment _env;
        public ProductController(IProductBusiness productBusiness, IConfiguration configuration, IWebHostEnvironment env) 
        {
            _productBusiness = productBusiness;
            _path = configuration["AppSettings:PATH"];
            _env = env;
        }



        [Route("get-all")]
        [HttpGet]
        public List<ProductsModel> GetAllProducts()
        {
            return _productBusiness.GetAllProducts();
        }


        [Route("search-product")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string product_name = ""; 
                if (formData.Keys.Contains("product_name") && !string.IsNullOrEmpty(Convert.ToString(formData["product_name"]))) { product_name = Convert.ToString(formData["product_name"]); }
                int fr_price = formData.ContainsKey("fr_price") && int.TryParse(formData["fr_price"].ToString(), out var fr_priceValue) ? fr_priceValue : 0;
                int to_price = formData.ContainsKey("to_price") && int.TryParse(formData["to_price"].ToString(), out var to_priceValue) ? to_priceValue : 0;
                long total = 0;
                var data = _productBusiness.Search(page, pageSize, out total, product_name, fr_price, to_price);
                return Ok(
                   new
                   {
                       TotalItems = total,
                       Data = data,
                       Page = page,
                       PageSize = pageSize
                   }
                   );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

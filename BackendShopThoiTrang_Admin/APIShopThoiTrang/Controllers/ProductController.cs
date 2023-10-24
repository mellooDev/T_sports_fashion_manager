using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
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

        //[Authorize(Roles = "2")]
        [Route("get-by-id/{id}")]
        [HttpGet]
        public ProductsModel GetProductbyId(string id)
        {
            return _productBusiness.GetProductbyId(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<ProductsModel> GetAllProducts()
        {
            return _productBusiness.GetAllProducts();
        }


        [Route("create-product")]
        [HttpPost]
        public ProductsModel CreateProduct([FromBody] ProductsModel model)
        {
            _productBusiness.Create(model);
            return model;
        }

        [Route("update-product")]
        [HttpPut]
        public ProductsModel UpdateProduct([FromBody] ProductsModel model)
        {
            _productBusiness.Update(model);
            return model;
        }

        [Route("delete-product")]
        [HttpPost]
        public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
        {
            string product_id = "";
            if (formData.Keys.Contains("product_id") && !string.IsNullOrEmpty(Convert.ToString(formData["product_id"]))) { product_id = Convert.ToString(formData["product_id"]); }
            _productBusiness.Delete(product_id);
            return Ok();
        }

        [NonAction]
        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"upload/{file.FileName}";
                    var fullPath = CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không tìm thấy");
            }
        }

        [Route("download")]
        [HttpPost]
        public IActionResult DownloadData([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var webRoot = _env.ContentRootPath;
                string exportPath = Path.Combine(webRoot + @"\Export\DM.xlsx");
                var stream = new FileStream(exportPath, FileMode.Open, FileAccess.Read);
                return File(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                int to_frice = formData.ContainsKey("to_frice") && int.TryParse(formData["to_frice"].ToString(), out var to_priceValue) ? to_priceValue : 0;
                long total = 0;
                var data = _productBusiness.Search(page, pageSize, out total, product_name, fr_price, to_frice);
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

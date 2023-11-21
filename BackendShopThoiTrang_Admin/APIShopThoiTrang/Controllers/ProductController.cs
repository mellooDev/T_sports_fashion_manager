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

        [Route("get-new-product")]
        [HttpGet]
        public List<ProductsModel> GetNewProducts()
        {
            return _productBusiness.GetNewProducts();
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
        [HttpDelete]
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


        [Route("search-product-by-name")]
        [HttpPost]
        public IActionResult SearchByName([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string product_name = ""; 
                if (formData.Keys.Contains("product_name") && !string.IsNullOrEmpty(Convert.ToString(formData["product_name"]))) { product_name = Convert.ToString(formData["product_name"]); }
                long total = 0;
                var data = _productBusiness.SearchByName(page, pageSize, out total, product_name);
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

        [Route("search-product-by-price")]
        [HttpPost]
        public IActionResult SearchByPrice([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                decimal fr_price = formData.ContainsKey("fr_price") && decimal.TryParse(formData["fr_price"].ToString(), out var fr_priceValue) ? fr_priceValue : 0;
                decimal to_price = formData.ContainsKey("to_price") && decimal.TryParse(formData["to_price"].ToString(), out var to_priceValue) ? to_priceValue : 0;
                long total = 0;
                var data = _productBusiness.SearchByPrice(page, pageSize, out total, fr_price, to_price);
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

        [Route("search-product-by-date")]
        [HttpPost]
        public IActionResult SearchByDate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                DateTime? fr_date = null;
                if (formData.Keys.Contains("fr_date") && formData["fr_date"] != null && formData["fr_date"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_date"].ToString());
                    fr_date = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_date = null;
                if (formData.Keys.Contains("to_date") && formData["to_date"] != null && formData["to_date"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_date"].ToString());
                    to_date = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                long total = 0;
                var data = _productBusiness.SearchByDate(page, pageSize, out total, fr_date, to_date);
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

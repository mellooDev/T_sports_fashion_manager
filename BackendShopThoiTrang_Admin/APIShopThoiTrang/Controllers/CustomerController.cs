using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBusiness _customerBusiness;
        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public CustomersModel GetCustomerbyId(string id)
        {
            return _customerBusiness.GetCustomerbyId(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<CustomersModel> GetAllCustomer()
        {
            return _customerBusiness.GetAllCustomer();
        }


        [Route("create-customer")]
        [HttpPost]
        public CustomersModel CreateBrand([FromBody] CustomersModel model)
        {
            _customerBusiness.Create(model);
            return model;
        }

        [Route("update-khach")]
        [HttpPost]
        public CustomersModel UpdateBrand([FromBody] CustomersModel model)
        {
            _customerBusiness.Update(model);
            return model;
        }

        [Route("delete-Khach")]
        [HttpPost]
        public IActionResult DeleteBrand([FromBody] Dictionary<string, object> formData)
        {
            string customer_id = "";
            if (formData.Keys.Contains("customer_id") && !string.IsNullOrEmpty(Convert.ToString(formData["customer_id"]))) { customer_id = Convert.ToString(formData["customer_id"]); }
            _customerBusiness.Delete(customer_id);
            return Ok();
        }
    }
}

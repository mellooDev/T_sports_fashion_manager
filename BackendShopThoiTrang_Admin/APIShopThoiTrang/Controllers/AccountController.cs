using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountBusiness _accountBusiness;
        public AccountController(IAccountBusiness adminBusiness) 
        {
            _accountBusiness = adminBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var account = _accountBusiness.Login(model.Username, model.Password);
            if (account == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { username = account.username, role = account.role_id, token = account.token });
        }

        [Route("signup")]
        [HttpPost]
        public AccountModel SignUp([FromBody] AccountModel model)
        {
            _accountBusiness.SignUp(model);
            return model;
        }

        [Route("get-acc-by-id/{id}")]
        [HttpGet]
        public AccountModel GetAccountByID(string id)
        {
            return _accountBusiness.GetAccountByID(id);
        }

        [Route("get-acc-by-username/{username}")]
        [HttpGet]
        public AccountModel GetAccountByUsername(string username)
        {
            return _accountBusiness.GetAccountByUsername(username);
        }

        [Route("create-account")]
        [HttpPost]
        public AccountModel Create([FromBody] AccountModel model)
        {
            _accountBusiness.Create(model);
            return model;
        }

        [Route("update-account")]
        [HttpPut]
        public AccountModel Update([FromBody] AccountModel model)
        {
            _accountBusiness.Update(model);
            return model;
        }

        [Route("delete-account")]
        [HttpDelete]
        public IActionResult Delete([FromBody] Dictionary<string, object> formData)
        {
            string account_id = "";
            if (formData.Keys.Contains("account_id") && !string.IsNullOrEmpty(Convert.ToString(formData["account_id"]))) { account_id = Convert.ToString(formData["account_id"]); }
            _accountBusiness.Delete(account_id);
            return Ok();
        }
    }
}

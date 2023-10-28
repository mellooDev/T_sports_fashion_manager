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
        private IAccountBusiness _adminBusiness;
        public AccountController(IAccountBusiness adminBusiness) 
        {
            _adminBusiness = adminBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var account = _adminBusiness.Login(model.Username, model.Password);
            if (account == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { username = account.username, email = account.email, address = account.address, role = account.role_id, token = account.token });
        }
    }
}

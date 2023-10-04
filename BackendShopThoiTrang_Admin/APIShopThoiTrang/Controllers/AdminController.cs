using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBusiness _adminBusiness;
        public AdminController(IAdminBusiness adminBusiness) 
        {
            _adminBusiness = adminBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var admin_account = _adminBusiness.Login(model.Username, model.Password);
            if (admin_account == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { username = admin_account.username, email = admin_account.email, address = admin_account.address, token = admin_account.token });
        }
    }
}

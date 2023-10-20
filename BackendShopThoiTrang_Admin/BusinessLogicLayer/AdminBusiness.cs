using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer
{
    public class AdminBusiness : IAdminBusiness
    {
        private IAccountRepository _res;
        private string secret;
        public AdminBusiness(IAccountRepository res, IConfiguration configuration)
        {
            _res = res;
            secret = configuration["AppSettings:Secret"];

        }

        public AccountModel Login(string username, string password)
        {
            var admin_account = _res.Login(username, password);
            if (admin_account == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin_account.username.ToString()),
                    new Claim(ClaimTypes.Email, admin_account.email),
                    new Claim(ClaimTypes.StreetAddress, admin_account.address.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            admin_account.token = tokenHandler.WriteToken(token);
            return admin_account;
        }
    }
}
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
    public class AccountBusiness : IAccountBusiness
    {
        private IAccountRepository _res;
        private string secret;
        public AccountBusiness(IAccountRepository res, IConfiguration configuration)
        {
            _res = res;
            secret = configuration["AppSettings:Secret"];

        }

        public AccountModel Login(string username, string password)
        {
            var account = _res.Login(username, password);
            if (account == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.username.ToString()),
                    new Claim(ClaimTypes.Role, account.role_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.token = tokenHandler.WriteToken(token);
            return account;
        }

        public AccountModel GetAccountByID(string id)
        {
            return _res.GetAccountByID(id);
        }

        public AccountModel GetAccountByUsername(string username)
        {
            return _res.GetAccountByUsername(username);
        }

        public bool SignUp(AccountModel model)
        {
            return _res.SignUp(model);
        }

        public bool Create(AccountModel model)
        {
            return _res.Create(model);
        }

        public bool Update(AccountModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }

    }
}
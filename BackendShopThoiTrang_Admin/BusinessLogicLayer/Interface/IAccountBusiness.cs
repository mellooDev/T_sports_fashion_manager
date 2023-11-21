using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface IAccountBusiness
    {
        AccountModel Login(string username, string password);

        AccountModel GetAccountByID(string id);

        AccountModel GetAccountByUsername(string username);

        bool SignUp(AccountModel account);

        bool Create(AccountModel account);

        bool Update(AccountModel account);

        bool Delete(string id);
    }
}

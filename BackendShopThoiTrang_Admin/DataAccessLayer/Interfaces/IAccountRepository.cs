using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IAccountRepository
    {
        AccountModel Login(string username, string password);

        //AccountModel GetAccountByID(string id);

        //AccountModel GetAccountByUsername(string username);

        //bool Create(AccountModel account);

        //bool Update(AccountModel account);

        //bool Delete(AccountModel account);

    }
}

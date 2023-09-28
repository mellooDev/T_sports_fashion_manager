using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface ICustomerBusiness
    {
        CustomersModel GetCustomerbyId(string id);

        List<CustomersModel> GetAllCustomer();

        bool Create(CustomersModel model);

        bool Update(CustomersModel model);

        bool Delete(string Id);
    }
}

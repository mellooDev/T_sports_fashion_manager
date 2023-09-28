using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _res;
        public CustomerBusiness(ICustomerRepository res)
        {
            _res = res;
        }
        public CustomersModel GetCustomerbyId(string id)
        {
            return _res.GetCustomerbyId(id);
        }

        public List<CustomersModel> GetAllCustomer()
        {
            return _res.GetAllCustomer();
        }

        public bool Create(CustomersModel model)
        {
            return _res.Create(model);
        }

        public bool Update(CustomersModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
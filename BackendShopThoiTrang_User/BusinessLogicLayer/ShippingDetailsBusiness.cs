using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class ShippingDetailsBusiness : IShippingDetailsBusiness
    {
        private IShippingDetailsRepository _res;
        public ShippingDetailsBusiness(IShippingDetailsRepository res)
        {
            _res = res;
        }
        public Shipping_detailsModel GetShippingbyId(string id)
        {
            return _res.GetShippingbyId(id);
        }

        public bool Create(Shipping_detailsModel model)
        {
            return _res.Create(model);
        }

        public bool Update(Shipping_detailsModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
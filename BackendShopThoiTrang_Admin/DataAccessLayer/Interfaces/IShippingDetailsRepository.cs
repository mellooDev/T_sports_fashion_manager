using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IShippingDetailsRepository
    {
        Shipping_detailsModel GetShippingbyId(string id);

        bool Create(Shipping_detailsModel model);

        bool Update(Shipping_detailsModel model);

        bool Delete(string Id);

    }
}

using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IOrderRepository
    {
        //Order_invoicesModel GetOrderbyId(string id);

        bool Create(Order_invoicesModel model);

        //bool Update(Order_invoicesModel model);

        //bool Delete(string Id);

    }
}

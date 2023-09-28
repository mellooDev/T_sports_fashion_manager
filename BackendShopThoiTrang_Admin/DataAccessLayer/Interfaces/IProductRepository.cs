using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IProductRepository
    {
        ProductsModel GetProductbyId(string id);

        List<ProductsModel> GetAllProducts();

        bool Create(ProductsModel model);

        bool Update(ProductsModel model);

        bool Delete(string Id);

    }
}

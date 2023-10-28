using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface IProductBusiness
    {
        ProductsModel GetProductbyId(string id);

        List<ProductsModel> GetAllProducts();

        bool Create(ProductsModel model);

        bool Update(ProductsModel model);

        bool Delete(string id);

        List<ProductsModel> Search(int pageIndex, int pageSize, out long total, string product_name, int fr_price, int to_price);
    }
}

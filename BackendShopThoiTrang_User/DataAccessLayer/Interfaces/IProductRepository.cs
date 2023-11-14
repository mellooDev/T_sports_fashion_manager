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

        List<ProductsModel> GetNewProducts();

        List<ProductsModel> Search(int pageIndex, int pageSize, out long total, string product_name, int fr_price, int to_price);

    }
}

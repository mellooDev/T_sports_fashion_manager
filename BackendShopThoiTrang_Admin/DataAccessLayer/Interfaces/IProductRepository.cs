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

        ProductsModel GetAllDetailsOfProductbyId(string id);

        List<ProductsModel> GetProductByCategoryName(int pageIndex, int pageSize, out long total, string name);

        List<ProductsModel> GetNewProducts();

        bool Create(ProductsModel model);

        bool Update(ProductsModel model);

        bool Delete(string Id);

        List<ProductsModel> SearchByName(int pageIndex, int pageSize, out long total, string product_name);

        List<ProductsModel> SearchByPrice(int pageIndex, int pageSize, out long total, decimal fr_price, decimal to_price);

        List<ProductsModel> SearchByDate(int pageIndex, int pageSize, out long total, DateTime? fr_date, DateTime? to_date);

    }
}

using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository _res;
        public ProductBusiness(IProductRepository res)
        {
            _res = res;
        }
        public ProductsModel GetProductbyId(string id)
        {
            return _res.GetProductbyId(id);
        }

        public ProductsModel GetAllDetailsOfProductbyId(string id)
        {
            return _res.GetAllDetailsOfProductbyId(id);
        }

        public List<ProductsModel> GetNewProducts()
        {
            return _res.GetNewProducts();
        }

        public List<ProductsModel> GetProductByCategoryName(int pageIndex, int pageSize, out long total, string name)
        {
            return _res.GetProductByCategoryName(pageIndex, pageSize, out total, name);
        }

        public bool Create(ProductsModel model)
        {
            return _res.Create(model);
        }

        public bool Update(ProductsModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }

        public List<ProductsModel> SearchByName(int pageIndex, int pageSize, out long total, string product_name)
        {
            return _res.SearchByName(pageIndex, pageSize, out total, product_name);
        }

        public List<ProductsModel> SearchByPrice(int pageIndex, int pageSize, out long total, decimal fr_price, decimal to_price)
        {
            return _res.SearchByPrice(pageIndex, pageSize, out total, fr_price, to_price);
        }

        public List<ProductsModel> SearchByDate(int pageIndex, int pageSize, out long total, DateTime? fr_date, DateTime? to_date)
        {
            return _res.SearchByDate(pageIndex, pageSize, out total, fr_date, to_date);
        }
    }
}
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

        public List<ProductsModel> GetAllProducts()
        {
            return _res.GetAllProducts();
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

        public List<ProductsModel> Search(int pageIndex, int pageSize, out long total, string product_name, int fr_price, int to_price)
        {
            return _res.Search(pageIndex, pageSize, out total, product_name, fr_price, to_price);
        }
    }
}
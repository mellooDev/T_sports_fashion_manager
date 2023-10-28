using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class BrandBusiness : IBrandBusiness
    {
        private IBrandRepository _res;
        public BrandBusiness(IBrandRepository res)
        {
            _res = res;
        }
        public BrandsModel GetBrandbyId(string id)
        {
            return _res.GetBrandbyId(id);
        }

        public BrandsModel GetProductbyBrandName(string name)
        {
            return _res.GetProductbyBrandName(name);
        }

        public List<BrandsModel> GetAllBrands()
        {
            return _res.GetAllBrands();
        }

        public bool Create(BrandsModel model)
        {
            return _res.Create(model);
        }

        public bool Update(BrandsModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string Id)
        {
            return _res.Delete(Id);
        }
    }
}
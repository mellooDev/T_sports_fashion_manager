using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IBrandRepository
    {
        BrandsModel GetBrandbyId(string id);

        List<ProductsModel> Search(int pageIndex, int pageSize, out long total, string brand_name);

        BrandsModel GetProductbyBrandName(string name);

        List<BrandsModel> GetAllBrands();

        bool Create(BrandsModel model);

        bool Update(BrandsModel model);

        bool Delete(string Id);

    }
}

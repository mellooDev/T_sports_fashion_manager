using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public partial interface IBrandBusiness
    {
        BrandsModel GetBrandbyId(string id);

        List<BrandsModel> GetAllBrands();

        bool Create(BrandsModel model);

        bool Update(BrandsModel model);

        bool Delete(string id);
    }
}

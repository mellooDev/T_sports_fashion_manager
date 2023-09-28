using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial interface IImportRepository
    {
        //ImportModel GetImportbyId(string id);

        bool Create(ImportModel model);

        //bool Update(ImportModel model);

        //bool Delete(string Id);

    }
}

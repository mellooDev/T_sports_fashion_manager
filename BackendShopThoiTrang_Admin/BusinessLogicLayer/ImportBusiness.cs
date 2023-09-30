using BusinessLogicLayer;
using DataAccessLayer;
using DataModel;

namespace BusinessLogicLayer
{
    public class ImportBusiness : IImportBusiness
    {
        private IImportRepository _res;
        public ImportBusiness(IImportRepository res)
        {
            _res = res;
        }
        //public ImportModel GetImportbyId(string id)
        //{
        //    return _res.GetImportbyId(id);
        //}

        public bool Create(ImportModel model)
        {
            return _res.Create(model);
        }

        //public bool Update(ImportModel model)
        //{
        //    return _res.Update(model);
        //}

        //public bool Delete(string Id)
        //{
        //    return _res.Delete(Id);
        //}
    }
}
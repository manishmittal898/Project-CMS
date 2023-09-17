using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupMaster
{
    public interface ILookupMasterService
    {
        Task<ServiceResponse<IEnumerable<LookupMasterViewModel>>> GetList(IndexModel model);
        ServiceResponse<LookupMasterViewModel> GetById(string id);
        Task<ServiceResponse<TblLookupMaster>> Save(LookupMasterPostModel model);
        Task<ServiceResponse<TblLookupMaster>> ActiveStatusUpdate(string id);

        Task<ServiceResponse<TblLookupMaster>> Delete(string id);
    }
}

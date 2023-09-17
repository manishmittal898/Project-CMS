using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.SubLookupMaster
{
    public interface ISubLookupMasterService
    {
        Task<ServiceResponse<IEnumerable<SubLookupMasterViewModel>>> GetList(IndexModel model);
        ServiceResponse<SubLookupMasterViewModel> GetById(string id);
        Task<ServiceResponse<TblSubLookupMaster>> Save(SubLookupMasterPostModel model);
        Task<ServiceResponse<TblSubLookupMaster>> ActiveStatusUpdate(string id);
        Task<ServiceResponse<TblSubLookupMaster>> Delete(string id);
    }
}

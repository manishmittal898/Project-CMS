using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.SubLookupMaster
{
    public interface ISubLookupMasterService
    {
        Task<ServiceResponse<IEnumerable<SubLookupMasterViewModel>>> GetList(IndexModel model);
        ServiceResponse<SubLookupMasterViewModel> GetById(long id);
        Task<ServiceResponse<TblSubLookupMaster>> Save(SubLookupMasterPostModel model);
        Task<ServiceResponse<TblSubLookupMaster>> ActiveStatusUpdate(long id);
        Task<ServiceResponse<TblSubLookupMaster>> Delete(long id);
    }
}

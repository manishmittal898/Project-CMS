using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupMaster
{
    public interface ILookupMasterService
    {
        Task<ServiceResponse<IEnumerable<LookupMasterViewModel>>> GetList(IndexModel model);
        ServiceResponse<LookupMasterViewModel> GetById(long id);
        Task<ServiceResponse<TblLookupMaster>> Save(LookupMasterPostModel model);
        Task<ServiceResponse<TblLookupMaster>> ActiveStatusUpdate(long id);

        Task<ServiceResponse<TblLookupMaster>> Delete(long id);
    }
}

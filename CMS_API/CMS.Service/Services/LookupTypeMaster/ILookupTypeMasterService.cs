using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupTypeMaster
{
   public interface ILookupTypeMasterService
    {
        Task<ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>>> GetListAsync(IndexModel model);
        ServiceResponse<TblLookupTypeMaster> GetById(int id);
        Task<ServiceResponse<TblLookupTypeMaster>> Save(LookupTypeMasterViewModel model);
        Task<ServiceResponse<TblLookupTypeMaster>> Edit(int id, LookupTypeMasterViewModel model);
        Task<ServiceResponse<TblLookupTypeMaster>> Delete(long id);
    }
}

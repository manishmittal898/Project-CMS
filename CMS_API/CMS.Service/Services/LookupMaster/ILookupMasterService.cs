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
        ServiceResponse<IEnumerable<Data.Models.TblLookupMaster>> GetList();
        ServiceResponse<TblLookupMaster> GetById(int id);
        Task<ServiceResponse<TblLookupMaster>> Save(LookupMasterViewModel model);
        Task<ServiceResponse<TblLookupMaster>> Edit(int id, LookupMasterViewModel model);
        Task<ServiceResponse<TblLookupMaster>> Delete(int id);
    }
}

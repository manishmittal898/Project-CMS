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
        ServiceResponse<IEnumerable<Data.Models.TblSubLookupMaster>> GetList();
        ServiceResponse<TblSubLookupMaster> GetById(int id);
        Task<ServiceResponse<TblSubLookupMaster>> Save(SubLookupMasterViewModel model);
        Task<ServiceResponse<TblSubLookupMaster>> Edit(int id, SubLookupMasterViewModel model);
        Task<ServiceResponse<TblSubLookupMaster>> Delete(int id);
    }
}

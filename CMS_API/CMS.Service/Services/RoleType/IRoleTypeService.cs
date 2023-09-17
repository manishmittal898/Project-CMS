using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.RoleType
{
    public interface IRoleTypeService
    {
        ServiceResponse<IEnumerable<Data.Models.TblRoleType>> GetList();
        ServiceResponse<Data.Models.TblRoleType> GetById(int id);
        Task<ServiceResponse<TblRoleType>> Save(RoleTypePostModel model);
        Task<ServiceResponse<TblRoleType>> Edit(int id, RoleTypePostModel model);
        Task<ServiceResponse<TblRoleType>> Delete(int id);
    }
}

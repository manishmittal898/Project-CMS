using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.RoleType
{
  public  interface IRoleTypeService
    {
        ServiceResponse<IEnumerable<Data.Models.IRoleType>> GetList();
        ServiceResponse<Data.Models.IRoleType> GetById(int id);
        Task<ServiceResponse<IRoleType>> Save(RoleTypePostModel model);
        Task<ServiceResponse<IRoleType>> Edit(int id, RoleTypePostModel model);
        Task<ServiceResponse<IRoleType>> Delete(int id);
    }
}

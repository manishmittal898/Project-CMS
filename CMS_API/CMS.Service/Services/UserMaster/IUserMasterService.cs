using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.User
{
    public interface IUserMasterService
    {
        ServiceResponse<IEnumerable<Data.Models.TblUserMaster>> GetList();
        ServiceResponse<TblUserMaster> GetById(int id);
        Task<ServiceResponse<TblUserMaster>> Save(UserViewPostModel model);
        Task<ServiceResponse<TblUserMaster>> Edit(int id, UserViewPostModel model);
        Task<ServiceResponse<TblUserMaster>> Delete(int id);
    }
}

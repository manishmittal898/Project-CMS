using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.User
{
    public interface IUserMasterService
    {

        Task<ServiceResponse<UserMasterViewModel>> GetById(long id);        
        Task<ServiceResponse<IEnumerable<UserMasterViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<TblUserMaster>> Save(UserMasterPostModel model);
        Task<ServiceResponse<TblUserMaster>> Delete(int id);
    }
}

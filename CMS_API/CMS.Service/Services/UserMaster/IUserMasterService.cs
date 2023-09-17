using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.User
{
    public interface IUserMasterService
    {

        Task<ServiceResponse<UserMasterViewModel>> GetById(long? id = null);
        Task<ServiceResponse<IEnumerable<UserMasterViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<TblUserMaster>> Save(UserMasterPostModel model);
        Task<ServiceResponse<TblUserMaster>> Delete(int id);
        Task<ServiceResponse<TblUserMaster>> UpdateProfilePic(UserProfilePostModel model);
        Task<ServiceResponse<TblUserMaster>> updateProfileDetail(UserDetailPostModel model);
    }
}

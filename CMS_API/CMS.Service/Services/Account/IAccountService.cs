using CMS.Core.ServiceHelper.Model;
using System.Threading.Tasks;
using static CMS.Service.Services.Account.AccountViewModel;

namespace CMS.Service.Services.Account
{
    public interface IAccountService
    {

        Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model);
        Task<ServiceResponse<string>> CheckUserExist(string loginId, bool isMobile, long id = 0);

        Task<ServiceResponse<string>> WebChangePassword(ChangePasswordModel model);
        Task<ServiceResponse<object>> LogoutUser(long id);
        ServiceResponse<string> GetEncryptedPassword(string value);
    }
}

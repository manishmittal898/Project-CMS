using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Service.Services.Account.AccountViewModel;

namespace CMS.Service.Services.Account
{
   public interface IAccountService
    {

         Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model);
         Task<ServiceResponse<string>> CheckUserExist(string mobileNumber);

         Task<ServiceResponse<string>> WebChangePassword(ChangePasswordModel model);
         Task<ServiceResponse<object>> LogoutUser(long id);
         ServiceResponse<string> GetEncrptedPassword(string value);
    }
}

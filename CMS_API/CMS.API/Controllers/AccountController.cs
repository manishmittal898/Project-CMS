using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Account;
using CMS.Service.Services.OTP;
using CMS.Service.Services.UserMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;
using static CMS.Service.Services.Account.AccountViewModel;

namespace CMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly IUserMasterService _user;
     
        public AccountController(IConfiguration config, IAccountService
        accountService, IUserMasterService user)
        {
            _accountService = accountService;
            _config = config;
            _user = user;
         
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model) => await _accountService.Login(model);
      

        // POST api/<UserController>
        [HttpPost]
        public async Task<object> Register([FromBody] UserMasterPostModel model)
        {
            if (ModelState.IsValid)
            {
                model.RoleId = (int)RoleEnum.Customer;
                return await _user.Save(model);

            }
            else
            {
                ServiceResponse<object> objReturn = new ServiceResponse<object>
                {
                    Message = "Invalid",
                    IsSuccess = false,
                    Data = null
                };

                return objReturn;
            }
            //return _roleTyp
        }



        //Post api/Account/WebChangePassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<string>> ChangePassword(ChangePasswordModel model) => await _accountService.WebChangePassword(model);


        [HttpGet]
        [AllowAnonymous]
        //Get api/Account/ValidateUserWithMobileNumber
        public async Task<ServiceResponse<string>> CheckUserExist(string loginId, bool isMobile, long userId) => await _accountService.CheckUserExist(loginId, isMobile, userId);


        [HttpGet]
        [AllowAnonymous]
        public ServiceResponse<string> GetEncryptedText(string value) => _accountService.GetEncryptedPassword(value);

        //Get api/Account/Logout
        [HttpGet]
        public async Task<ServiceResponse<object>> Logout(long id) => await _accountService.LogoutUser(id);

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<LoginResponseModel>> SocialLogin(SocialLoginModel model) => await _accountService.Login(model);


    }
}

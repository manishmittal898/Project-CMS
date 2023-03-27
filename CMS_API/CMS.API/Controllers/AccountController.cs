using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static CMS.Service.Services.Account.AccountViewModel;

namespace CMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private IAccountService _accountService;

        public AccountController(IConfiguration config, IAccountService
        accountService)
        {
            _accountService = accountService;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model)
        {
            return await _accountService.Login(model);
        }


        //Post api/Account/WebChangePassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<string>> ChangePassword(ChangePasswordModel model)
        {
            return await _accountService.WebChangePassword(model);
        }


        [HttpGet]
        [AllowAnonymous]
        //Get api/Account/ValidateUserWithMobileNumber
        public async Task<ServiceResponse<string>> ValidateUserWithMobileNUmber(string mobileNumber)
        {
            return await _accountService.CheckUserExist(mobileNumber);
        }

        [HttpGet]
        [AllowAnonymous]
        public ServiceResponse<string> GetEncrptedText(string value)
        {
            return _accountService.GetEncrptedPassword(value);
        }
        //Get api/Account/Logout
        [HttpGet]
        public async Task<ServiceResponse<object>> Logout(long id)
        {
            return await _accountService.LogoutUser(id);
        }

       
    }
}

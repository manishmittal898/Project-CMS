﻿using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Account;
using CMS.Service.Services.OTP;
using CMS.Service.Services.User;
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
        private IConfiguration _config;
        private IAccountService _accountService;
        private readonly IUserMasterService _user;
        private IOTPService _oTPService;

        public AccountController(IConfiguration config, IAccountService
        accountService, IUserMasterService user, IOTPService oTPService)
        {
            _accountService = accountService;
            _config = config;
            _user = user;
            _oTPService = oTPService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model)
        {
            return await _accountService.Login(model);
        }

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
                ServiceResponse<object> objReturn = new ServiceResponse<object>();
                objReturn.Message = "Invalid";
                objReturn.IsSuccess = false;
                objReturn.Data = null;

                return objReturn;
            }
            //return _roleTyp
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
        public async Task<ServiceResponse<string>> CheckUserExist(string loginId, bool isMobile,long userId)
        {
            return await _accountService.CheckUserExist(loginId,  isMobile, userId);
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

        [HttpGet]
        public async Task<ServiceResponse<string>> RequestOTP(string emailId)
        {
            return await _oTPService.GenerateOTP(emailId);
        }


    }
}

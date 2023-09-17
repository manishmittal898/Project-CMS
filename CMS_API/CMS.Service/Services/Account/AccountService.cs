using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.OTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;
using static CMS.Service.Services.Account.AccountViewModel;


namespace CMS.Service.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {

        DB_CMSContext _db;
        IOTPService _otpService;
        public AccountService(DB_CMSContext db, IConfiguration _configuration, IOTPService otpService) : base(_configuration)
        {
            _db = db;
            _otpService = otpService;
        }
        public async Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model)
        {
            ServiceResponse<LoginResponseModel> ResponseObject = new ServiceResponse<LoginResponseModel>();
            LoginResponseModel response = new LoginResponseModel();
            try
            {

                var user = await _db.TblUserMasters.Where(x => x.Email.ToLower().Equals(model.Email.Trim()) && x.IsActive.Value && !x.IsDeleted).Include(x => x.Role).FirstOrDefaultAsync();

                if (user != null && user.Password.Equals(model.Password) && ((model.Plateform == PlatformEnum.Customer.GetStringValue() && user.RoleId == (int)RoleEnum.Customer) || (model.Plateform == PlatformEnum.Admin.GetStringValue() && user.RoleId < (int)RoleEnum.Customer)))
                {

                    var fresh_token = _security.CreateToken(user.UserId, model.Email, user.Role.RoleName, user.RoleId, false);
                    response.UserId = user.UserId;
                    response.Token = fresh_token;
                    response.RoleId = user.RoleId;
                    response.UserName = user.Email;
                    response.RoleName = user.Role.RoleName;
                    response.RoleLevel = user.Role.RoleLevel;
                    response.ProfilePhoto = !string.IsNullOrEmpty(user.ProfilePhoto) ? user.ProfilePhoto.ToAbsolutePath() : null;
                    response.FullName = user.FirstName + ' ' + user.LastName;

                    await SaveUserLog(user.UserId, response);
                }
                else if (user != null && !user.Password.Equals(model.Password))
                {
                    return CreateResponse<LoginResponseModel>(null, "Incorrect Username or Password...", false, ((int)ApiStatusCode.RecordNotFound));

                }
                else if (user != null)
                {
                    return CreateResponse<LoginResponseModel>(null, "You are not authorised to access " + model.Plateform + " Portal.", false, ((int)ApiStatusCode.RecordNotFound));

                }
                else
                {
                    return CreateResponse<LoginResponseModel>(null, "You have not register with us,Please Signup", false, ((int)ApiStatusCode.RecordNotFound));
                }
                return CreateResponse<LoginResponseModel>(response, "Login Successful", true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                return CreateResponse<LoginResponseModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ServiceResponse<string>> CheckUserExist(string loginId, bool isMobile, long id = 0)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(loginId))
                {
                    var user = await _db.TblUserMasters.Where(x => (isMobile ? x.Mobile == loginId : x.Email == loginId) && (id == 0 || x.UserId != id) && !x.IsDeleted).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        return CreateResponse<string>(null, "User already exist", true, ((int)ApiStatusCode.AlreadyExist));
                    }
                    return CreateResponse<string>(null, "User not exist with system", true, ((int)ApiStatusCode.Ok));
                }
                return CreateResponse<string>(null, "User not exist with system", true, ((int)ApiStatusCode.BadRequest));
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<ServiceResponse<string>> WebChangePassword(ChangePasswordModel model)
        {
            try
            {
                var encrptPassword = _security.EncryptData(model.Password);
                var user = await _db.TblUserMasters.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                var otp = _otpService.VerifyOTP(new OTPVerifyModel { SessionId = model.SessionID, OTP = model.OTP });
                if (otp.IsSuccess && !(bool)otp.Data)
                {
                    return CreateResponse<string>(null, ResponseMessage.OTPMissMatch, false, ((int)ApiStatusCode.OTPVarificationFailed));

                }
                else if (user != null)
                {
                    user.Password = encrptPassword;
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.Email, "Password update successful", true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ServiceResponse<object>> LogoutUser(long id)
        {
            try
            {
                var isExist = await _db.TblUserMasters.Where(x => x.UserId == id).FirstOrDefaultAsync();
                if (isExist != null)
                {

                    await SaveUserLog(isExist.UserId);
                    return CreateResponse<object>(true, ResponseMessage.Logout, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<object>(false, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<object>(false, ex.Message ?? ex.InnerException.ToString(), false, ((int)ApiStatusCode.ServerException));
            }
        }
        public ServiceResponse<string> GetEncrptedPassword(string value)
        {
            ServiceResponse<string> objModel = new ServiceResponse<string>();
            try
            {
                var encrptPassword = _security.EncryptData(value);
                objModel.Data = encrptPassword;
                objModel.IsSuccess = true;
                return objModel;
            }
            catch (Exception ex)
            {
                objModel.Data = "";
                objModel.IsSuccess = false;
                objModel.Message = ex.Message + "-- " + ex.InnerException.Message;
                return objModel;
            }
        }
        /// <summary>
        /// Save Log
        /// </summary>
        /// <param name="userId">Logged in UserId</param>
        /// <param name="model">pass while Login</param>
        /// <returns></returns>
        private async Task<ServiceResponse<TblUserMasterLog>> SaveUserLog(long userId, LoginResponseModel model = null)
        {
            try
            {
                var OldActiveSession = await _db.TblUserMasterLogs.Where(x => x.UserId == userId && x.SessionEndTime == null).ToListAsync();
                if (OldActiveSession.Count > 0)
                {
                    foreach (var item in OldActiveSession)
                    {
                        item.SessionEndTime = DateTime.Now;
                    }
                    _db.TblUserMasterLogs.UpdateRange(OldActiveSession);
                    await _db.SaveChangesAsync();
                }
                TblUserMasterLog objLog = new TblUserMasterLog();
                if (model != null)
                {
                    objLog.UserId = model.UserId;
                    objLog.Token = model.Token;
                    await _db.TblUserMasterLogs.AddAsync(objLog);
                    await _db.SaveChangesAsync();

                }
                return CreateResponse(objLog, ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserMasterLog>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }



    }
}

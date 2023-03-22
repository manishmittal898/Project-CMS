using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Service.Services.Account.AccountViewModel;


namespace CMS.Service.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {

        private readonly Security _security;
        DB_CMSContext _db;
        public AccountService(DB_CMSContext db, IConfiguration _configuration)
        {

            _security = new Security(_configuration);
            _db = db;


        }
        public async Task<ServiceResponse<LoginResponseModel>> Login(LoginModel model)
        {
            ServiceResponse<LoginResponseModel> ResponseObject = new ServiceResponse<LoginResponseModel>();
            LoginResponseModel response = new LoginResponseModel();
            try
            {
                var user = await _db.TblUserMasters.Where(x => x.Email.ToLower().Equals(model.Email) && x.Password.Equals(_security.DecryptData(model.Password)) && x.IsActive.Value && !x.IsDeleted).Include(x => x.Role).FirstOrDefaultAsync();

                if (user != null)
                {

                    var fresh_token = _security.CreateToken(user.UserId, model.Email, user.Role.RoleName, user.RoleId, false);
                    response.UserId = user.UserId;
                    response.Token = fresh_token.Data;
                    response.RoleId = user.RoleId;
                    response.UserName = user.Email;
                    response.RoleName = user.Role.RoleName;
                    response.RoleLevel = user.Role.RoleLevel;
                    response.ProfilePhoto = !string.IsNullOrEmpty(user.ProfilePhoto) ? user.ProfilePhoto.ToAbsolutePath() : null;
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
        public async Task<ServiceResponse<string>> CheckUserExist(string email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var user = await _db.TblUserMasters.Where(x => x.Email == email && !x.IsDeleted).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        return CreateResponse<string>(null, "User email already exist", true, ((int)ApiStatusCode.AlreadyExist));
                    }
                    return CreateResponse<string>(null, "User email not exist with system", true, ((int)ApiStatusCode.Ok));
                }
                return CreateResponse<string>(null, "User email not exist with system", true, ((int)ApiStatusCode.BadRequest));
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
                var user = await _db.TblUserMasters.Where(x => x.Mobile == model.Email).FirstOrDefaultAsync();
                if (user != null)
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

                    // await _db.SaveChangesAsync();
                    return CreateResponse<object>(true, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
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
    }
}

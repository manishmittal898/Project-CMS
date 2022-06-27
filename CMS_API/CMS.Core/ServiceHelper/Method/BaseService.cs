using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CMS.Core.ServiceHelper.Method
{
    public class BaseService
    {
        public readonly LoginUserViewModel _loginUserDetail;
        public BaseService()
        {
            _loginUserDetail = new LoginUserViewModel();
            SetLoginUserDetail();
        }

        public virtual ServiceResponse<T> CreateResponse<T>(T objData, string Message, bool IsSuccess, int statusCode= (int)ApiStatusCode.Ok, string exception = "", string validationMessage = "", long? TotalRecord = null) where T : class
        {
            ServiceResponse<T> objReturn = new ServiceResponse<T>();
            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.Data = objData;
            objReturn.Exception = exception;
            objReturn.StatusCode = statusCode;
            objReturn.TotalRecord = TotalRecord > 0 ? TotalRecord : null;
            return objReturn;
        }

        public class LoginUserViewModel
        {
            public long? UserId { get; set; }
            public int? RoleId { get; set; }
            public string RoleName { get; set; }
            
            public string UserName { get; set; }

            public DateTime? LoginTime { get; set; }
            public LoginUserViewModel()
            {

              

            }
        }

        private void SetLoginUserDetail()
        {
            try
            {
                var user = new HttpContextAccessor()?.HttpContext?.User;

                if (user != null && user.Claims.Count() > 0)
                {

                    _loginUserDetail.UserId = user.HasClaim(x => x.Type == TokenClaimsConstant.UserId) ? (long?)Convert.ToInt64(user.FindFirst(TokenClaimsConstant.UserId).Value) : null;

                    _loginUserDetail.UserName = user.HasClaim(x => x.Type == TokenClaimsConstant.UserName) ? user.FindFirst(TokenClaimsConstant.UserName).Value : null;

                    _loginUserDetail.RoleId = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleId) ? (int?)Convert.ToInt32(user.FindFirst(TokenClaimsConstant.RoleId).Value) : 0;

                    _loginUserDetail.RoleName = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleName) ? user.FindFirst(TokenClaimsConstant.RoleName).Value : null;

                    _loginUserDetail.LoginTime = user.HasClaim(x => x.Type == TokenClaimsConstant.GenerateTime) ? (DateTime?)DateTime.ParseExact(user.FindFirst(TokenClaimsConstant.GenerateTime).Value, "dd-mm-yyyy HH:mm:ss", null) : null;

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

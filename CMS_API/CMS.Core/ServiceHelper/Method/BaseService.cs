﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace CMS.Core.ServiceHelper.Method
{
    public class BaseService
    {
        public readonly LoginUserViewModel _loginUserDetail;
        public readonly Security _security;
        public readonly EmailHelper _emailHelper;

        public BaseService(IConfiguration _configuration)
        {
            _loginUserDetail = SetLoginUserDetail();
            _security = new Security(_configuration);
            _emailHelper = new EmailHelper(_configuration);
        }

        public virtual ServiceResponse<T> CreateResponse<T>(T objData, string Message, bool IsSuccess, int statusCode = (int)ApiStatusCode.Ok, string exception = "", string validationMessage = "", long? TotalRecord = null) where T : class
        {
            ServiceResponse<T> objReturn = new ServiceResponse<T>
            {
                Message = Message,
                IsSuccess = IsSuccess,
                Data = objData,
                Exception = exception,
                StatusCode = statusCode,
                TotalRecord = TotalRecord > 0 ? TotalRecord : null
            };
            return objReturn;
        }

        public class LoginUserViewModel
        {
            public long? UserId { get; set; }
            public int? RoleId { get; set; }
            public string RoleName { get; set; }

            public string UserName { get; set; }

            public DateTime? LoginTime { get; set; }

        }

        private LoginUserViewModel SetLoginUserDetail()
        {
            try
            {
                System.Security.Claims.ClaimsPrincipal user = new HttpContextAccessor()?.HttpContext?.User;
                LoginUserViewModel objUser = new LoginUserViewModel();
                if (user != null && user.Claims.Count() > 0)
                {

                    objUser.UserId = user.HasClaim(x => x.Type == TokenClaimsConstant.UserId) ? (long?)Convert.ToInt64(user.FindFirst(TokenClaimsConstant.UserId).Value) : null;

                    objUser.UserName = user.HasClaim(x => x.Type == TokenClaimsConstant.UserName) ? user.FindFirst(TokenClaimsConstant.UserName).Value : null;

                    objUser.RoleId = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleId) ? Convert.ToInt32(user.FindFirst(TokenClaimsConstant.RoleId).Value) : 0;

                    objUser.RoleName = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleName) ? user.FindFirst(TokenClaimsConstant.RoleName).Value : null;

                    objUser.LoginTime = user.HasClaim(x => x.Type == TokenClaimsConstant.GenerateTime) ? (DateTime?)DateTime.ParseExact(user.FindFirst(TokenClaimsConstant.GenerateTime).Value, "dd-mm-yyyy HH:mm:ss", null) : null;

                }
                return objUser;

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

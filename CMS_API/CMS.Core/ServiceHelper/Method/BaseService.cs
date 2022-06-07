using CMS.Core.ServiceHelper.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.ServiceHelper.Method
{
    public class BaseService
    {
        public readonly LoginUserViewModel _loginUserDetail;
        public BaseService()
        {
            _loginUserDetail = new LoginUserViewModel();
        }

        public virtual ServiceResponse<T> CreateResponse<T>(T objData, string Message, bool IsSuccess, string exception = "", string validationMessage = "") where T : class
        {
            ServiceResponse<T> objReturn = new ServiceResponse<T>();
            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.Data = objData;
            objReturn.Exception = exception;

            return objReturn;
        }

        public class LoginUserViewModel
        {
            public int UserId { get; set; }
            public int RoleTypeId { get; set; }
            public string RoleType { get; set; }
            public int BaseRoleTypeId { get; set; }

            public string BaseRoleType { get; set; }
            public string Name { get; set; }


            public LoginUserViewModel()
            {

              

            }
        }
    }
}

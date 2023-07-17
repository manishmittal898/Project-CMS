//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

//namespace CMS.Core.ServiceHelper.Filter
//{
   
//    public class AuthorizeFilter : IAuthorizationFilter
//    {
//        readonly string[] _claim;

//        public AuthorizeFilter(params string[] claim)
//        {
//            _claim = claim;
//        }

//        public bool AllowMultiple => throw new NotImplementedException();

//        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
//        {
//            throw new NotImplementedException();
//        }
         
//    }
//}

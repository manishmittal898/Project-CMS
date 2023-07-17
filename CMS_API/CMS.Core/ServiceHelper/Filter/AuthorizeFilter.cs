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
//    public class AuthorizeAttribute : TypeFilterAttribute
//    {
//        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
//        {
//            Arguments = new object[] { claim };
//        }
//    }

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

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
//            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

//            if (IsAuthenticated)
//            {
//                bool flagClaim = false;
//                foreach (var item in _claim)
//                {
//                    if (context.HttpContext.User.HasClaim("Role", item))
//                        flagClaim = true;
//                }
//                if (!flagClaim)
//                {
//                    if (context.HttpContext.Request.IsAjaxRequest())
//                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Set HTTP 401   
//                    else
//                        context.Result = new RedirectResult("~/Login/Index");
//                }
//            }
//            else
//            {
//                if (context.HttpContext.Request.IsAjaxRequest())
//                {
//                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Set HTTP 403 -   
//                }
//                else
//                {
//                    context.Result = new RedirectResult("~/Login/Index");
//                }
//            }
//            return;
//        }
//    }
//}

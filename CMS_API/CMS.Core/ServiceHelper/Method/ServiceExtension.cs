using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace CMS.Core.ServiceHelper.ExtensionMethod
{
    public static class ServiceExtension
    {
        private static IHttpContextAccessor _httpContext;
        static ServiceExtension()
        {
            _httpContext = new HttpContextAccessor();
        }
     
        public static string ToAbsolutePath(this string filePath)

        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    HttpRequest request = _httpContext.HttpContext.Request;

                    return string.Concat(request.IsHttps ? "https://" : "http://", request.HttpContext.Request.Host.Value, filePath.Replace("~", "").Replace(@"\", @"/").Replace(@"//", @"/"));

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }


        }

      
    }
}

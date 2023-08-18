using CMS.Core.FixedValue;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
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
            return generateAbsolutePath(filePath);

        }

        public static string ToAbsolutePath(this string filePath, string thumnailPath)

        {
            return generateAbsolutePath(filePath, thumnailPath);

        }

        private static string generateAbsolutePath(string filePath, string thumnailPath = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    HttpRequest request = _httpContext.HttpContext.Request;

                    if (!string.IsNullOrEmpty(thumnailPath) && filePath.Contains(".webp"))
                    {
                        var k = filePath.Split("\\").ToList();
                        k.Insert(k.Count - 1, thumnailPath);
                        string checkPath = Path.Join(k.ToArray());
                        if (File.Exists(checkPath))
                        {
                            filePath = Path.Combine("\\", checkPath);
                        }

                    }

                    return string.Concat(request.IsHttps ? "https://" : "http://", request.HttpContext.Request.Host.Value, filePath.Replace("~", "").Replace(@"\", @"/").Replace(@"//", @"/"));

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string getSizePath(Size size)
        {
            if (size.Width == ImageSize.Large.Width && size.Height == ImageSize.Large.Height)
            {
                return string.Concat("Large_Thumbnail");


            }
            else if (size.Width == ImageSize.Medium.Width && size.Height == ImageSize.Medium.Height)
            {
                return string.Concat("Medium_Thumbnail");


            }
            else 
            {
                return string.Concat("Small_Thumbnail");

            }
        }
    }
}

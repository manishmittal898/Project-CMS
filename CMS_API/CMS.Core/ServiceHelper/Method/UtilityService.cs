using HeyRed.Mime;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.IO;

namespace CMS.Core.ServiceHelper.Method
{
    public static class UtilityService
    {
        private static readonly HostingEnvironment _env = new HostingEnvironment();

        /// <summary>
        /// Save File from base64 string
        /// </summary>
        /// <param name="base64str">base64 string of File</param>
        /// <param name="filePath">save location file path</param>
        /// <param name="fileName">file name if required custom name</param>
        /// <returns></returns>
        public static string SaveFile(string base64str, string filePath, string fileName)

        {
            string saveFile = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(base64str) && !string.IsNullOrEmpty(filePath))
                {
                    string[] Fileinfo = base64str.Split(';');
                    byte[] byteArr = Convert.FromBase64String(Fileinfo[1][(Fileinfo[1].IndexOf(',') + 1)..]);

                    saveFile = filePath;
                    filePath = filePath.GetPhysicalPath();
                    if (!Directory.Exists(filePath))
                    {
                        _ = Directory.CreateDirectory(filePath);
                    }
                    fileName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() + base64str.GetFileExtension() : fileName;
                    File.WriteAllBytes(filePath + fileName, byteArr);
                    saveFile = string.Concat(saveFile, "/", fileName);
                }
            }
            catch
            {
                throw;
            }
            return saveFile;
        }

        public static string GetFile(string filePath)
        {
            string base64 = string.Empty;
            try
            {
                filePath = filePath.GetPhysicalPath();

                if (File.Exists(filePath))
                {
                    base64 = "Data:" + GetMimeType(filePath) + ";base64,";
                    ;
                    byte[] bytarr = File.ReadAllBytes(Path.Combine(_env.ContentRootPath, filePath));
                    base64 += Convert.ToBase64String(bytarr);
                }
            }
            catch
            {
                throw;
            }
            return base64;
        }


        private static string GetFileExtension(this string base64String)
        {
            string ext;
            try
            {
                string mime = base64String.Split(';')[0].Split(':')[1];
                ext = MimeTypesMap.GetExtension(mime);

            }
            catch (Exception)
            {

                throw;
            }
            return ext;

        }

        private static string GetPhysicalPath(this string path)
        {

            try
            {

                return Path.Combine(_env.ContentRootPath, path.Replace("~", ""));


            }
            catch (Exception)
            {

                throw;
            }


        }

        private static string GetMimeType(string filePath)
        {
            try
            {
                string[] Path = filePath.Split('\\');
                return MimeTypesMap.GetMimeType(Path[^1]);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}

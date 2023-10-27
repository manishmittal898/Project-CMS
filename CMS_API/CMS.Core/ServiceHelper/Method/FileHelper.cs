using CMS.Core.FixedValue;
using HeyRed.Mime;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS.Core.ServiceHelper.Method
{
    public class FileHelper
    {
        private static IHostingEnvironment _env;
        public FileHelper(IHostingEnvironment environment)
        {
            _env = environment;
        }

        /// <summary>
        /// Save File from base64 string
        /// </summary>
        /// <param name="base64str">base64 string of File</param>
        /// <param name="filePath">save location file path</param>
        /// <param name="fileName">file name if required custom name</param>
        /// <returns></returns>

        public async Task<string> Save(string base64str, string filePath, string fileName = null, bool withFilePath = true, bool isThumbnail = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(base64str))
                {
                    base64str = base64str.Replace(" ", "");
                    base64str = Regex.Replace(base64str, @"^\s*$\n", string.Empty, RegexOptions.Multiline).TrimEnd();
                    string path = GetPhysicalPath(filePath);
                    if (!Directory.Exists(path))
                    {
                        _ = Directory.CreateDirectory(path);
                    }

                    if (IsBase64(base64str) && !string.IsNullOrEmpty(filePath))
                    {

                        byte[] byteArr;
                        if (base64str.Split(';').Length > 0)
                        {
                            string[] Fileinfo = base64str.Split(';');
                            byteArr = Convert.FromBase64String(Fileinfo[1][(Fileinfo[1].IndexOf(',') + 1)..]);
                        }
                        else
                        {
                            byteArr = Convert.FromBase64String(base64str);
                        }
                        string[] imageExt = { ".jpg", ".png", ".jpeg", ".bmp", ".tiff" };
                        if (imageExt.Contains(GetFileExtension(base64str)))
                        {
                            fileName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() + ".webp" : fileName.Split(".").Length > 1 ? fileName.Replace(" ", "_") : fileName.Replace(" ", "_") + ".webp";

                            byte[] webPBytArr = ConvertImageToWebP(byteArr);
                            File.WriteAllBytes(Path.Combine(path, fileName), webPBytArr);

                            if (isThumbnail)
                            {
                                Size[] sizes = { ImageSize.Small, ImageSize.Medium };
                                for (int i = 0; i < sizes.Length; i++)
                                {

                                    string thumbnailPath = string.Concat(path, "\\", ServiceExtension.getSizePath(sizes[i]));
                                    if (!Directory.Exists(thumbnailPath))
                                    {
                                        _ = Directory.CreateDirectory(thumbnailPath);
                                    }

                                    byte[] webPThumbnailBytArr = ConvertImageToWebP(byteArr, sizes[i].Width, sizes[i].Height);
                                    File.WriteAllBytes(Path.Combine(thumbnailPath, fileName), webPThumbnailBytArr);
                                }


                            }

                        }
                        else
                        {
                            fileName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() + GetFileExtension(base64str) : fileName.Split(".").Length > 1 ? fileName.Replace(" ", "_") : fileName.Replace(" ", "_") + GetFileExtension(base64str);
                            File.WriteAllBytes(Path.Combine(path, fileName), byteArr);


                        }
                        //  saveFile = filePath;
                        return withFilePath ? string.Concat(filePath, fileName) : fileName;
                    }
                    else
                    {
                        bool result = Uri.TryCreate(base64str, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                        if (result)
                        {
                            return uriResult.AbsolutePath.Replace("/", "\\");
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public string Save(IFormFile file, string filePath, string fileName = null)
        {

            try
            {
                if (file != null && !string.IsNullOrEmpty(filePath))
                {
                    string path = GetPhysicalPath(filePath);

                    fileName = string.IsNullOrEmpty(fileName) ? Path.GetFileName(file.FileName) : fileName.Replace(" ", "_");

                    if (!Directory.Exists(path))
                    {
                        _ = Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    return fileName;
                }

            }
            catch (Exception)
            {
            }
            return null;

        }

        public string Get(string filePath)
        {
            string base64 = string.Empty;
            try
            {
                filePath = GetPhysicalPath(filePath);
                _env = new HostingEnvironment();
                if (File.Exists(filePath))
                {
                    base64 = "Data:" + GetMimeType(filePath) + ";base64,";

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

        public bool Delete(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath = GetPhysicalPath(filePath);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }
                }

            }
            catch
            {
            }

            return false;
        }

        private string GetFileExtension(string base64String)
        {
            string ext;
            try
            {

                string mime = base64String.Split(';')[0].Split(':')[1];
                ext = MimeTypesMap.GetExtension(mime);

                if (ext == "bin")
                {
                    ext = mime.Split("/")[1].ToLower();

                }

                ext = string.Concat(".", ext);
            }
            catch (Exception)
            {

                throw;
            }
            return ext;

        }

        private string GetPhysicalPath(string path)
        {
            try
            {
                return string.Concat(_env.ContentRootPath, path.Replace("~", "\\"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetMimeType(string filePath)
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
        private bool IsBase64(string base64String)
        {

            try
            {
                base64String = Regex.Replace(base64String, @"^\s*$\n", string.Empty).TrimEnd();


                if (base64String.Split(';').Length > 0)
                {
                    string[] Fileinfo = base64String.Split(';');
                    base64String = Fileinfo[1][(Fileinfo[1].IndexOf(',') + 1)..];
                }

                if (string.IsNullOrEmpty(base64String) || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r"))
                { return false; }


                _ = Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {

                return false;
            }

        }

        private byte[] ConvertImageToWebP(byte[] imageData, int newWidth = 0, int newHeight = 0)
        {
            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            using MemoryStream inputStream = new MemoryStream(imageData);
            _ = newWidth > 0 && newHeight > 0
                ? imageFactory.Load(inputStream).Format(new WebPFormat()).Quality(90).Resize(new Size(newWidth, newHeight))
                : imageFactory.Load(inputStream).Format(new WebPFormat()).Quality(90);


            using MemoryStream outputStream = new MemoryStream();
            _ = imageFactory.Save(outputStream);
            byte[] webpBytes = outputStream.ToArray();
            return webpBytes;
        }

    }

}

using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMS.Core.ServiceHelper.ExtensionMethod
{
    public class Security : BaseService
    {
        IConfiguration _configuration;
        public Security(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptData(string strValue)
        {
            try
            {
                byte[] key = { }; //Encryption Key   
                byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
                byte[] inputByteArray;
                string strKey = _configuration.GetValue<string>("EncryptionKey");

                key = Encoding.UTF8.GetBytes(strKey);
                // DESCryptoServiceProvider is a cryptography class defind in c#.  
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strValue);
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }
            catch
            {
                throw;
            }
        }

        public string DecryptData(string strValue)
        {
            byte[] key = { };// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[strValue.Length];
            string strKey = _configuration.GetValue<string>("EncryptionKey");
            try
            {
                key = Encoding.UTF8.GetBytes(strKey);
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strValue);

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(Objmst.ToArray());
            }
            catch
            {
                throw;
            }
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string UniqueId() 
        {
            string result;
            string prefix = "AUR";
            var month = DateTime.Now.Month;
            var year = DateTime.Now.ToString("yy");
            Random random = new Random();
            result = prefix + "/" + month.ToString() + "/" + year + "/" + random.Next(10000, 199999).ToString();
            return result;
        }
        public ServiceResponse<string> CreateToken(long UserId, string UserName, string RoleType, int RoleId, bool isWeb=true)
        {

            var key = _configuration.GetValue<string>("Jwt:Key");
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]  { new Claim(TokenClaimsConstant.UserId, UserId.ToString()),
                new Claim(TokenClaimsConstant.UserName, UserName),
                new Claim(TokenClaimsConstant.RoleName, RoleType),
                new Claim(TokenClaimsConstant.RoleId, RoleId.ToString()),
                new Claim(TokenClaimsConstant.GenerateTime, DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss")),
                new Claim(TokenClaimsConstant.UniqueId,  Guid.NewGuid().ToString())

               
            };
            var token = new JwtSecurityToken(issuer, issuer, claims, expires: isWeb ? DateTime.Now.AddHours(10) : DateTime.Now.AddDays(90),
                  signingCredentials: credentials);

            return CreateResponse<string>(new JwtSecurityTokenHandler().WriteToken(token), ResponseMessage.Success, true);
        }

    }
}

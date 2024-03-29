﻿using CMS.Core.FixedValue;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Core.ServiceHelper.Method
{
    public class Security
    {
        private readonly IConfiguration _configuration;
        // private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private readonly int BlockSize = 128;
        public Security(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public string EncryptData(string strValue)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(strValue);

        //    string strKey = _configuration.GetValue<string>("EncryptionKey");
        //    HashAlgorithm hash = MD5.Create();


        //    try
        //    {
        //        SymmetricAlgorithm crypt = Aes.Create();
        //        crypt.BlockSize = BlockSize;
        //        crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(strKey));
        //        //   crypt.IV = IV;
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream =
        //               new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cryptoStream.Write(bytes, 0, bytes.Length);
        //            }

        //            return Convert.ToBase64String(memoryStream.ToArray());
        //        }


        //        //byte[] key = { }; //Encryption Key   
        //        //byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        //        //byte[] inputByteArray;
        //        //string strKey = _configuration.GetValue<string>("EncryptionKey");

        //        //key = Encoding.UTF8.GetBytes(strKey);
        //        //// DESCryptoServiceProvider is a cryptography class defind in c#.  
        //        //DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
        //        //inputByteArray = Encoding.UTF8.GetBytes(strValue);
        //        //MemoryStream Objmst = new MemoryStream();
        //        //CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
        //        //Objcs.Write(inputByteArray, 0, inputByteArray.Length);
        //        //Objcs.FlushFinalBlock();

        //        //return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public string EncryptData(string strValue)
        {
            return Encrypt(strValue);

        }
        public string EncryptData(long strValue)
        {
            return Encrypt(strValue.ToString());

        }
        private string Encrypt(string strValue)
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("EncryptionKey"));

            try
            {
                using AesManaged aes = new AesManaged();
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                //  aes.Padding = PaddingMode.PKCS7;
                using ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(strValue);
                byte[] encryptedData = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                return Base64UrlEncoder.Encode(encryptedData);
                //  return Convert.ToBase64String(encryptedData);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DecryptData(string strValue)
        {

            byte[] key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("EncryptionKey"));
            byte[] encryptedData = Base64UrlEncoder.DecodeBytes(strValue);
            // byte[] encryptedData = Convert.FromBase64String(strValue);


            try
            {
                using AesManaged aes = new AesManaged();
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                // aes.Padding = PaddingMode.PKCS7;
                using ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return Encoding.UTF8.GetString(decryptedData);

            }
            catch (Exception)
            {

                throw;
            }

        }


        //public string DecryptData(string strValue)
        //{



        //    string strKey = _configuration.GetValue<string>("EncryptionKey");
        //    byte[] bytes = Convert.FromBase64String(strValue);
        //    SymmetricAlgorithm crypt = Aes.Create();
        //    HashAlgorithm hash = MD5.Create();
        //    crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(strKey));
        //    //  crypt.IV = IV;
        //    try
        //    {



        //        using (MemoryStream memoryStream = new MemoryStream(bytes))
        //        {
        //            using (CryptoStream cryptoStream =
        //               new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
        //            {
        //                byte[] decryptedBytes = new byte[bytes.Length];
        //                cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
        //                return Encoding.Unicode.GetString(decryptedBytes);
        //            }
        //        }


        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string UniqueId()
        {
            string result;
            string prefix = "AUR";
            int month = DateTime.Now.Month;
            string year = DateTime.Now.ToString("yy");
            Random random = new Random();
            result = prefix + "/" + month.ToString() + "/" + year + "/" + random.Next(10000, 199999).ToString();
            return result;
        }
        public string CreateToken(long UserId, string UserName, string RoleType, int RoleId, bool isWeb = true)
        {

            string key = _configuration.GetValue<string>("Jwt:Key");
            string issuer = _configuration.GetValue<string>("Jwt:Issuer");

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]  { new Claim(TokenClaimsConstant.UserId, UserId.ToString()),
                new Claim(TokenClaimsConstant.UserName, UserName),
                new Claim(TokenClaimsConstant.RoleName, RoleType),
                new Claim(TokenClaimsConstant.RoleId, RoleId.ToString()),
                new Claim(TokenClaimsConstant.GenerateTime, DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss")),
                new Claim(TokenClaimsConstant.UniqueId,  Guid.NewGuid().ToString()),
                  new Claim(ClaimTypes.Role,  RoleType)

            };

            System.Collections.Generic.List<string> data = Enum.GetNames(typeof(RoleEnum)).ToList();

            // Add roles as multiple claims
            foreach (string role in data)
            {
                _ = claims.Append(new Claim(ClaimTypes.Role, role));
            }
            JwtSecurityToken token = new JwtSecurityToken(issuer, issuer, claims, expires: isWeb ? DateTime.Now.AddHours(10) : DateTime.Now.AddDays(90),
                  signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

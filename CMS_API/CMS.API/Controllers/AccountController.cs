using CMS.Service.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private IUserMasterService _userMaster;

        public AccountController(IConfiguration config, IUserMasterService userMaster)
        {
            _userMaster = userMaster;
            _config = config;
        }
        [HttpGet]
        public string getToken()
        {
            var obj = new
            {
                Username = "kuldeep",
                UserType = "Admin",
                EmailAddress = "kuldeep@nisha.com"
            };


            return GenerateJSONWebToken(obj);
        }


        /// <summary>
        /// this method is use for create token when user is authentcate using db value
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(dynamic userInfo)
        {
          

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Name, userInfo.Username),
        new Claim(JwtRegisteredClaimNames.Typ, userInfo.UserType),
        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Account;
using CMS.Service.Services.OTP;
using CMS.Service.Services.UserMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static CMS.Core.FixedValue.Enums;
using static CMS.Service.Services.Account.AccountViewModel;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OTPController : ControllerBase
    {

        private readonly IOTPService _oTPService;

        public OTPController(IOTPService oTPService)
        {

            _oTPService = oTPService;
        }


        [HttpGet]
        public async Task<ServiceResponse<string>> GetOTP(string emailId) => await _oTPService.GenerateOTP(emailId);


        [HttpPost]
        public async Task<ServiceResponse<object>> VerifyOTP(OTPVerifyModel model) => await _oTPService.VerifyOTP(model);




    }
}

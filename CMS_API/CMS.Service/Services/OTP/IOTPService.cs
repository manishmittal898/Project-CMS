using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.OTP
{
    public interface IOTPService
    {
        public Task<ServiceResponse<string>> GenerateOTP(string SendOn);
        public bool VerifyOTP(string sessionId, string OTP);


    }
}

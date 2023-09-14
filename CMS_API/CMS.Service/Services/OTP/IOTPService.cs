using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Service.Services.OTP
{
    public interface IOTPService
    {
        public string GenerateOTP(string SendOn);
        public bool VerifyOTP(string sessionId,string OTP);


    }
}

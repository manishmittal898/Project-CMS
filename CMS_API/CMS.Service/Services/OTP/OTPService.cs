using CMS.Core.ServiceHelper.Method;
using CMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Service.Services.OTP
{
    public class OTPService : BaseService, IOTPService
    {
        DB_CMSContext _db;
        private readonly EmailHelper _emailHelper;

        public OTPService(DB_CMSContext db, EmailHelper emailHelper, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _emailHelper = emailHelper;
        }
        public string GenerateOTP(string SendOn)
        {
            try
            {
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                TblUserOtpdatum otpdatum = new TblUserOtpdatum();


                otpdatum.SendOn = SendOn;
                otpdatum.Otp = _security.EncryptData(r);
                otpdatum.Attempt = 1;

                var result = _db.TblUserOtpdata.Add(otpdatum);
                _db.SaveChanges();
                _emailHelper.SendEmailAsync(new MailRequest { ToEmail = "sandeep.suthar08@gmail.com", Body = $"your OTP is {r}" });
                return result.Entity.SessionId.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();

        }

        public bool VerifyOTP(string sessionId, string OTP)
        {
            TblUserOtpdatum otpdatum = _db.TblUserOtpdata.Where(x => x.SessionId.ToString() == sessionId && !x.IsVerified).FirstOrDefault();
            if (otpdatum != null && OTP == _security.DecryptData(otpdatum.Otp))
            {
                otpdatum.Attempt = otpdatum.Attempt + 1;
                otpdatum.IsVerified = true;
                return true;
            }
            else if (otpdatum != null && OTP == _security.DecryptData(otpdatum.Otp))
            {
                otpdatum.Attempt = otpdatum.Attempt + 1;
                return false;
            }
            else
            {
                return false;

            }
        }
    }
}

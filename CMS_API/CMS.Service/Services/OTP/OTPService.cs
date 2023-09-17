using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

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
        public async Task<ServiceResponse<string>> GenerateOTP(string SendOn)
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
                await _emailHelper.SendEmailAsync(new MailRequest { ToEmail = "sandeep.suthar08@yopmail.com", Body = $"your OTP is {r}", Subject = "OTP Verification" });

                return CreateResponse(result.Entity.SessionId.ToString(), ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError);

            }


        }

        public ServiceResponse<object> VerifyOTP(OTPVerifyModel model)
        {
            bool IsSuccess = false;
            TblUserOtpdatum otpdatum = _db.TblUserOtpdata.Where(x => x.SessionId.ToString() == model.SessionId&& !x.IsVerified).FirstOrDefault();
            if (otpdatum != null &&model.OTP == _security.DecryptData(otpdatum.Otp))
            {
                otpdatum.Attempt = otpdatum.Attempt + 1;
                otpdatum.IsVerified = true;
                IsSuccess = true;
            }
            else if (otpdatum != null && model.OTP != _security.DecryptData(otpdatum.Otp))
            {
                otpdatum.Attempt = otpdatum.Attempt + 1;

            }
            _db.TblUserOtpdata.Update(otpdatum);
            _db.SaveChanges();
            return CreateResponse<object>(IsSuccess, IsSuccess ? ResponseMessage.Success : ResponseMessage.InvalidData, true, (IsSuccess ? (int)ApiStatusCode.Ok : (int)ApiStatusCode.OtpInvalid));

        }
    }
    public class OTPVerifyModel
    {
        public string SessionId { get; set; }
        public string OTP { get; set; }
        
    }
}

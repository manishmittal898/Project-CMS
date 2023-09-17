using CMS.Core.ServiceHelper.Model;
using System.Threading.Tasks;

namespace CMS.Service.Services.OTP
{
    public interface IOTPService
    {
        public Task<ServiceResponse<string>> GenerateOTP(string SendOn);
        public ServiceResponse<object> VerifyOTP(OTPVerifyModel model);


    }
}

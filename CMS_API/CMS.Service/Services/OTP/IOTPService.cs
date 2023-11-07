using CMS.Core.ServiceHelper.Model;
using System.Threading.Tasks;

namespace CMS.Service.Services.OTP
{
    public interface IOTPService
    {
         Task<ServiceResponse<string>> GenerateOTP(string SendOn);
         Task<ServiceResponse<object>> VerifyOTP(OTPVerifyModel model);

         ServiceResponse<object> VerifySessionOTP(OTPVerifyModel model);
    }
}

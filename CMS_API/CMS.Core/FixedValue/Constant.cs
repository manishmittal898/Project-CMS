using System.Drawing;

namespace CMS.Core.FixedValue
{

    public class Constants
    {
        public const string CULTURE_KEY = "Culture";
        public const string ALLOW_ALL_ORIGINS = "AllowAllOrigins";
        public const string SMTP_SERVER = "MailSettings";

        public const string SMS_AuthKey = "SMS:AuthKey";
        public const string SMS_SanderId = "SMS:SanderId";
        public const string SMS_EndPoint = "SMS:EndPoint";
        public const string JWT_Key = "Jwt:Key";
        public const string JWT_ISSUER = "Jwt:Issuer";


#if DEBUG
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Development";

        public const string CONNECTION_STRING = "ConnectionStrings:Development";
#else
        public const string CONNECTION_STRING = "ConnectionStrings:Production"; 
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Production";
        
#endif

    }

    public static class TokenClaimsConstant
    {
        public const string GenerateTime = "GenerateTime";
        public const string UniqueId = "UniqueId";
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string RoleName = "RoleName";
        public const string RoleId = "RoleId";

    }

    public enum ApiStatusCode
    {
        Ok = 200,
        RecordNotFound = 204,
        AlreadyExist = 205,
        NotFound = 404,
        UnAuthorized = 401,
        InternalServerError = 501,
        ServerException = 405,
        DataBaseTransactionFailed = 406,
        InvaildModel = 407,
        BadRequest = 408,
        UnApproved = 409,
        OtpInvalid = 410,
        OTPVarificationFailed = 411,
        OTPValidityExpire = 412,
    }


    public static class ImageSize
    {
        public static Size Small { get; } = new Size(150, 200);
        public static Size Medium { get; } = new Size(520, 693);
        public static Size Large { get; } = new Size(1080, 1440);


    }
}

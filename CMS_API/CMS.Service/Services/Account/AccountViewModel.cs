namespace CMS.Service.Services.Account
{
    public class AccountViewModel
    {
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Plateform { get; set; }
        }
        public class LoginResponseModel
        {
            public long UserId { get; set; }
            public int RoleId { get; set; }
            public int? RoleLevel { get; set; }
            public string Token { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }

            public string RoleName { get; set; }
            public string ProfilePhoto { get; set; }

        }

        public class ChangePasswordModel
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public string SessionID { get; set; }
            public string OTP { get; set; }
        }
    }
}

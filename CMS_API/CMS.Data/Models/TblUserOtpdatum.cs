using System;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserOtpdatum
    {
        public Guid SessionId { get; set; }
        public string SendOn { get; set; }
        public string Otp { get; set; }
        public DateTime SentAt { get; set; }
        public int Attempt { get; set; }
        public bool IsVerified { get; set; }
    }
}

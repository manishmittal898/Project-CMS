using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserMasterLog
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime? SessionEndTime { get; set; }

        public virtual TblUserMaster User { get; set; }
    }
}

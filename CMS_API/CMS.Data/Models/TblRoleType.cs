using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblRoleType
    {
        public TblRoleType()
        {
            TblUserMasters = new HashSet<TblUserMaster>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? RoleLevel { get; set; }
        public int? ParentRoleId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual ICollection<TblUserMaster> TblUserMasters { get; set; }
    }
}

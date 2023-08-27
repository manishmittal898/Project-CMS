using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblLookupTypeMaster
    {
        public TblLookupTypeMaster()
        {
            TblLookupMasters = new HashSet<TblLookupMaster>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string EnumValue { get; set; }
        public bool IsValue { get; set; }
        public bool IsSubLookup { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsImage { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual ICollection<TblLookupMaster> TblLookupMasters { get; set; }
    }
}

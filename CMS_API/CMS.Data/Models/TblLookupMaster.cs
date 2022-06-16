using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblLookupMaster
    {
        public TblLookupMaster()
        {
            TblProductMasters = new HashSet<TblProductMaster>();
            TblSubLookupMasters = new HashSet<TblSubLookupMaster>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? SortedOrder { get; set; }
        public long? LookUpType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblLookupTypeMaster LookUpTypeNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasters { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
    }
}

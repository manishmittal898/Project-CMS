using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblLookupMaster
    {
        public TblLookupMaster()
        {
            TblSubLookupMasters = new HashSet<TblSubLookupMaster>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortedOrder { get; set; }
        public int? LookUpType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblLookupTypeMaster LookUpTypeNavigation { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
    }
}

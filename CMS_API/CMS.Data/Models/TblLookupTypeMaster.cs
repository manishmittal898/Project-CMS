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

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<TblLookupMaster> TblLookupMasters { get; set; }
    }
}

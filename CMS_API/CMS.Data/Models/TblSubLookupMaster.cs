using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblSubLookupMaster
    {
        public TblSubLookupMaster()
        {
            TblProductMasters = new HashSet<TblProductMaster>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? SortedOrder { get; set; }
        public long LookUpId { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblLookupMaster LookUp { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasters { get; set; }
    }
}

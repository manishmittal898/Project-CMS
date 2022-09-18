using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblLookupMaster
    {
        public TblLookupMaster()
        {
            TblCmspageContentMasters = new HashSet<TblCmspageContentMaster>();
            TblProductMasterCaptionTags = new HashSet<TblProductMaster>();
            TblProductMasterCategories = new HashSet<TblProductMaster>();
            TblProductStocks = new HashSet<TblProductStock>();
            TblSubLookupMasters = new HashSet<TblSubLookupMaster>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
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
        public virtual ICollection<TblCmspageContentMaster> TblCmspageContentMasters { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterCaptionTags { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterCategories { get; set; }
        public virtual ICollection<TblProductStock> TblProductStocks { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
    }
}

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
            TblProductMasterColors = new HashSet<TblProductMaster>();
            TblProductMasterDiscounts = new HashSet<TblProductMaster>();
            TblProductMasterFabrics = new HashSet<TblProductMaster>();
            TblProductMasterLengths = new HashSet<TblProductMaster>();
            TblProductMasterOccasions = new HashSet<TblProductMaster>();
            TblProductMasterPatterns = new HashSet<TblProductMaster>();
            TblProductMasterViewSections = new HashSet<TblProductMaster>();
            TblProductStocks = new HashSet<TblProductStock>();
            TblSubLookupMasters = new HashSet<TblSubLookupMaster>();
            TblUserAddressMasterAddressTypeNavigations = new HashSet<TblUserAddressMaster>();
            TblUserAddressMasterStates = new HashSet<TblUserAddressMaster>();
            TblUserCartLists = new HashSet<TblUserCartList>();
            TblUserMasters = new HashSet<TblUserMaster>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
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
        public virtual ICollection<TblProductMaster> TblProductMasterColors { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterDiscounts { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterFabrics { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterLengths { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterOccasions { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterPatterns { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterViewSections { get; set; }
        public virtual ICollection<TblProductStock> TblProductStocks { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
        public virtual ICollection<TblUserAddressMaster> TblUserAddressMasterAddressTypeNavigations { get; set; }
        public virtual ICollection<TblUserAddressMaster> TblUserAddressMasterStates { get; set; }
        public virtual ICollection<TblUserCartList> TblUserCartLists { get; set; }
        public virtual ICollection<TblUserMaster> TblUserMasters { get; set; }
    }
}

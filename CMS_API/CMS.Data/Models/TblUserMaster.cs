using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserMaster
    {
        public TblUserMaster()
        {
            TblCmspageContentMasterCreatedByNavigations = new HashSet<TblCmspageContentMaster>();
            TblCmspageContentMasterModifiedByNavigations = new HashSet<TblCmspageContentMaster>();
            TblFileDataMasterCreatedByNavigations = new HashSet<TblFileDataMaster>();
            TblFileDataMasterModifiedByNavigations = new HashSet<TblFileDataMaster>();
            TblGecategoryMaterCreatedByNavigations = new HashSet<TblGecategoryMater>();
            TblGecategoryMaterModifiedByNavigations = new HashSet<TblGecategoryMater>();
            TblGeneralEntryCreatedByNavigations = new HashSet<TblGeneralEntry>();
            TblGeneralEntryModifiedByNavigations = new HashSet<TblGeneralEntry>();
            TblLookupMasterCreatedByNavigations = new HashSet<TblLookupMaster>();
            TblLookupMasterModifiedByNavigations = new HashSet<TblLookupMaster>();
            TblLookupTypeMasterCreatedByNavigations = new HashSet<TblLookupTypeMaster>();
            TblLookupTypeMasterModifiedByNavigations = new HashSet<TblLookupTypeMaster>();
            TblProductImageCreatedByNavigations = new HashSet<TblProductImage>();
            TblProductImageModifiedByNavigations = new HashSet<TblProductImage>();
            TblProductMasterCreatedByNavigations = new HashSet<TblProductMaster>();
            TblProductMasterModifiedByNavigations = new HashSet<TblProductMaster>();
            TblProductReviews = new HashSet<TblProductReview>();
            TblRoleTypeCreatedByNavigations = new HashSet<TblRoleType>();
            TblRoleTypeModifiedByNavigations = new HashSet<TblRoleType>();
            TblSubLookupMasterCreatedByNavigations = new HashSet<TblSubLookupMaster>();
            TblSubLookupMasterModifiedByNavigations = new HashSet<TblSubLookupMaster>();
            TblUserAddressMasterCreatedByNavigations = new HashSet<TblUserAddressMaster>();
            TblUserAddressMasterModifiedByNavigations = new HashSet<TblUserAddressMaster>();
            TblUserAddressMasterUsers = new HashSet<TblUserAddressMaster>();
            TblUserCartLists = new HashSet<TblUserCartList>();
            TblUserMasterLogs = new HashSet<TblUserMasterLog>();
            TblUserWishLists = new HashSet<TblUserWishList>();
        }

        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public long? GenderId { get; set; }
        public int RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblLookupMaster Gender { get; set; }
        public virtual TblRoleType Role { get; set; }
        public virtual ICollection<TblCmspageContentMaster> TblCmspageContentMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblCmspageContentMaster> TblCmspageContentMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblFileDataMaster> TblFileDataMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblFileDataMaster> TblFileDataMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblGecategoryMater> TblGecategoryMaterCreatedByNavigations { get; set; }
        public virtual ICollection<TblGecategoryMater> TblGecategoryMaterModifiedByNavigations { get; set; }
        public virtual ICollection<TblGeneralEntry> TblGeneralEntryCreatedByNavigations { get; set; }
        public virtual ICollection<TblGeneralEntry> TblGeneralEntryModifiedByNavigations { get; set; }
        public virtual ICollection<TblLookupMaster> TblLookupMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblLookupMaster> TblLookupMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblLookupTypeMaster> TblLookupTypeMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblLookupTypeMaster> TblLookupTypeMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblProductImage> TblProductImageCreatedByNavigations { get; set; }
        public virtual ICollection<TblProductImage> TblProductImageModifiedByNavigations { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblProductMaster> TblProductMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
        public virtual ICollection<TblRoleType> TblRoleTypeCreatedByNavigations { get; set; }
        public virtual ICollection<TblRoleType> TblRoleTypeModifiedByNavigations { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblUserAddressMaster> TblUserAddressMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblUserAddressMaster> TblUserAddressMasterModifiedByNavigations { get; set; }
        public virtual ICollection<TblUserAddressMaster> TblUserAddressMasterUsers { get; set; }
        public virtual ICollection<TblUserCartList> TblUserCartLists { get; set; }
        public virtual ICollection<TblUserMasterLog> TblUserMasterLogs { get; set; }
        public virtual ICollection<TblUserWishList> TblUserWishLists { get; set; }
    }
}

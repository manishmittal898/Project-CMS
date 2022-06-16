using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserMaster
    {
        public TblUserMaster()
        {
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
            TblSubLookupTypeMasterCreatedByNavigations = new HashSet<TblSubLookupTypeMaster>();
            TblSubLookupTypeMasterModifiedByNavigations = new HashSet<TblSubLookupTypeMaster>();
        }

        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblRoleType Role { get; set; }
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
        public virtual ICollection<TblSubLookupTypeMaster> TblSubLookupTypeMasterCreatedByNavigations { get; set; }
        public virtual ICollection<TblSubLookupTypeMaster> TblSubLookupTypeMasterModifiedByNavigations { get; set; }
    }
}

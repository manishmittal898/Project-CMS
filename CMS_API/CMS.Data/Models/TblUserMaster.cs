using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserMaster
    {
        public TblUserMaster()
        {
            TblProductImages = new HashSet<TblProductImage>();
            TblProductReviews = new HashSet<TblProductReview>();
            TblSubLookupMasters = new HashSet<TblSubLookupMaster>();
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

        public virtual IRoleType Role { get; set; }
        public virtual ICollection<TblProductImage> TblProductImages { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
    }
}

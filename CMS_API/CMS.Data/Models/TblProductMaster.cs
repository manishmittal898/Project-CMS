using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductMaster
    {
        public TblProductMaster()
        {
            TblProductReviews = new HashSet<TblProductReview>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public string Desc { get; set; }
        public decimal? Price { get; set; }
        public long? CaptionTagId { get; set; }
        public string Summary { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual TblLookupMaster CaptionTag { get; set; }
        public virtual TblLookupMaster Category { get; set; }
        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual TblSubLookupMaster SubCategory { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
    }
}

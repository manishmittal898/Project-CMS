using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductMaster
    {
        public TblProductMaster()
        {
            TblProductImages = new HashSet<TblProductImage>();
            TblProductReviews = new HashSet<TblProductReview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal? Price { get; set; }
        public string Caption { get; set; }
        public string Summary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblProductImage> TblProductImages { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
    }
}

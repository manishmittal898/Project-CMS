﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductMaster
    {
        public TblProductMaster()
        {
            TblProductReviews = new HashSet<TblProductReview>();
            TblProductStocks = new HashSet<TblProductStock>();
            TblUserCartLists = new HashSet<TblUserCartList>();
            TblUserWishLists = new HashSet<TblUserWishList>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string Desc { get; set; }
        public decimal? Price { get; set; }
        public decimal? SellingPrice { get; set; }
        public long? CaptionTagId { get; set; }
        public long? ViewSectionId { get; set; }
        public long? Discount { get; set; }
        public long? OccasionId { get; set; }
        public long? FabricId { get; set; }
        public long? LengthId { get; set; }
        public long? ColorId { get; set; }
        public long? PatternId { get; set; }
        public string UniqueId { get; set; }
        public string Summary { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Keyword { get; set; }

        public virtual TblLookupMaster CaptionTag { get; set; }
        public virtual TblLookupMaster Category { get; set; }
        public virtual TblLookupMaster Color { get; set; }
        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblLookupMaster Fabric { get; set; }
        public virtual TblLookupMaster Length { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual TblLookupMaster Occasion { get; set; }
        public virtual TblLookupMaster Pattern { get; set; }
        public virtual TblSubLookupMaster SubCategory { get; set; }
        public virtual TblLookupMaster ViewSection { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
        public virtual ICollection<TblProductStock> TblProductStocks { get; set; }
        public virtual ICollection<TblUserCartList> TblUserCartLists { get; set; }
        public virtual ICollection<TblUserWishList> TblUserWishLists { get; set; }
    }
}

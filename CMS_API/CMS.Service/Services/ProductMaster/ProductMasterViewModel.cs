using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterPostModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public string Desc { get; set; }
        [Range(0, 9999999999999)]
        public decimal? Price { get; set; }
        [Range(0, 9999999999999)]
        public decimal? SellingPrice { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string CaptionTagId { get; set; }
        public string ViewSectionId { get; set; }
        public string DiscountId { get; set; }
        public string OccasionId { get; set; }
        public string FabricId { get; set; }
        public string LengthId { get; set; }
        public string ColorId { get; set; }
        public string PatternId { get; set; }
        public string UniqueId { get; set; }
        public string Summary { get; set; }
        public string Keyword { get; set; }
        public List<string> Files { get; set; }
        public List<ProductStockModel> Stocks { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
    }

    public class ProductMasterViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string Desc { get; set; }

        public decimal? Price { get; set; }
        public decimal? SellingPrice { get; set; }
        public string CaptionTagId { get; set; }
        public string ViewSectionId { get; set; }
        
        public long Discount { get; set; }
        public string OccasionId { get; set; }
        public string Occasion { get; set; }
        public string FabricId { get; set; }
        public string Fabric { get; set; }
        public string LengthId { get; set; }
        public string Length { get; set; }
        public string ColorId { get; set; }
        public string Color { get; set; }
        public string PatternId { get; set; }
        public string Pattern { get; set; }
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
        public string CaptionTag { get; set; }
        public string ViewSection { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public bool IsWhishList { get; set; }
        public List<ProductImageViewModel> Files { get; set; }
        public List<ProductStockModel> Stocks { get; set; }

    }
    public class ProductImageViewModel
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string ProductId { get; set; }
    }

    public class ProductCategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

    }

    public class ProductStockModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string SizeId { get; set; }
        public string Size { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public long Discount { get; set; }
        public int? Quantity { get; set; }

    }


    public class ProductFilterModel : IndexModel
    {
        public List<string> CategoryId { get; set; }
        public List<string> SubCategoryId { get; set; }
        public List<long?> Price { get; set; }
        public bool IsAvailableStock { get; set; }
        public List<string> SizeId { get; set; }
        public List<string> ViewSectionId { get; set; }
        public string Keyword { get; set; }
        public List<string> Ids { get; set; }
        public List<string> CaptionTagId { get; set; }
        public long? Discount { get; set; }
        public List<string> OccasionId { get; set; }
        public List<string> FabricId { get; set; }
        public List<string> LengthId { get; set; }
        public List<string> ColorId { get; set; }
        public List<string> PatternId { get; set; }
        public string UniqueId { get; set; }

    }


}

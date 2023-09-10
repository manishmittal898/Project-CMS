using CMS.Service.Services.ProductMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Service.Services.UserCartProduct
{
    public class UserCartProductViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string SizeId { get; set; }
        public string Size { get; set; }

        public long Quantity { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        public decimal? Price { get; set; }
        public decimal? SellingPrice { get; set; }

        public string DiscountId { get; set; }
        public string Discount { get; set; }
        public string UniqueId { get; set; }
        public string CaptionTag { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTime AddedOn { get; set; }

    }

    public class UserCartProductPostModel
    {
        public string ProductId { get; set; }

        public string SizeId { get; set; }
        public long Quantity { get; set; }

    }
}

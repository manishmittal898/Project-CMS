using CMS.Service.Services.ProductMaster;
using System;

namespace CMS.Service.Services.UserCartProduct
{
    public class UserCartProductViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string SizeId { get; set; }
        public string Size { get; set; }

        public long Quantity { get; set; }
        public DateTime AddedOn { get; set; }
        public ProductMasterViewModel Product { get; set; }

    }

    public class UserCartProductPostModel
    {
        public string ProductId { get; set; }
        public string SizeId { get; set; }
        public long Quantity { get; set; }

    }

    public class UpdateCartItemPostModel
    {
        public string Id { get; set; }
       public long Quantity { get; set; }
    }
}

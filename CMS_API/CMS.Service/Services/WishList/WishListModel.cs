using CMS.Service.Services.ProductMaster;
using System;

namespace CMS.Service.Services.WishList
{
    public class WishListViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ProductMasterViewModel Product { get; set; }
    }

    public class WishListPostModel
    {
        public string ProductId { get; set; }
    }
}

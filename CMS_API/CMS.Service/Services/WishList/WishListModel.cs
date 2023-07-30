using CMS.Service.Services.ProductMaster;
using System;

namespace CMS.Service.Services.WishList
{
    public class WishListViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual ProductMasterViewModel Product { get; set; }
    }

    public class WishListPostModel
    {
        public long ProductId { get; set; }
    }
}

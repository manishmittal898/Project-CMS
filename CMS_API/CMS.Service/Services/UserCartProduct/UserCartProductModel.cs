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
        public DateTime AddedOn { get; set; }

        public virtual ProductMasterViewModel Product { get; set; }
    }

    public class UserCartProductPostModel
    {
        public string ProductId { get; set; }
    }
}

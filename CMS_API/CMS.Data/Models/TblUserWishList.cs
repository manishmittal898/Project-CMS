﻿using System;


namespace CMS.Data.Models
{
    public partial class TblUserWishList
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual TblProductMaster Product { get; set; }
        public virtual TblUserMaster User { get; set; }
    }
}

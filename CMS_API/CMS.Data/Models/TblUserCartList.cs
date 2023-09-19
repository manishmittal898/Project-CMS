using System;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserCartList
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public long Quantity { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual TblProductMaster Product { get; set; }
        public virtual TblLookupMaster Size { get; set; }
        public virtual TblUserMaster User { get; set; }
    }
}

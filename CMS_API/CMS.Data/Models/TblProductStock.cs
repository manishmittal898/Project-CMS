#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductStock
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }

        public virtual TblProductMaster Product { get; set; }
        public virtual TblLookupMaster Size { get; set; }
    }
}

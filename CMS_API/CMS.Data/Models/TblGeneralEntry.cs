using System;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblGeneralEntry
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string DataId { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public int? SortedOrder { get; set; }
        public string Keyword { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblGecategoryMater Category { get; set; }
        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
    }
}

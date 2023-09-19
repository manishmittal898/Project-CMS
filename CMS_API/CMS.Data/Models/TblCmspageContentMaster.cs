using System;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblCmspageContentMaster
    {
        public long Id { get; set; }
        public long PageId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public int? SortedOrder { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual TblLookupMaster Page { get; set; }
    }
}

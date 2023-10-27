using System;


namespace CMS.Data.Models
{
    public partial class TblFileDataMaster
    {
        public long Id { get; set; }
        public string DataId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblUserAddressMaster
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string BuildingNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public long? StateId { get; set; }
        public long? AddressType { get; set; }
        public bool IsPrimary { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual TblLookupMaster AddressTypeNavigation { get; set; }
        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual TblLookupMaster State { get; set; }
        public virtual TblUserMaster User { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductMaster
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal? Price { get; set; }
        public string Caption { get; set; }
        public string Summary { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

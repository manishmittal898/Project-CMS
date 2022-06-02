﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Data.Models
{
    public partial class TblProductImage
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public long? FilePath { get; set; }
        public int? ProductId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblProductMaster Product { get; set; }
    }
}

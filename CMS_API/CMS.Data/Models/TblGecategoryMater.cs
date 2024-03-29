﻿using System;
using System.Collections.Generic;


namespace CMS.Data.Models
{
    public partial class TblGecategoryMater
    {
        public TblGecategoryMater()
        {
            TblGeneralEntries = new HashSet<TblGeneralEntry>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string EnumValue { get; set; }
        public string ImagePath { get; set; }
        public bool IsShowInMain { get; set; }
        public bool IsShowUrl { get; set; }
        public bool IsShowThumbnail { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }
        public bool IsSystemEntry { get; set; }
        public int ContentType { get; set; }
        public int? SortedOrder { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual TblUserMaster CreatedByNavigation { get; set; }
        public virtual TblUserMaster ModifiedByNavigation { get; set; }
        public virtual ICollection<TblGeneralEntry> TblGeneralEntries { get; set; }
    }
}

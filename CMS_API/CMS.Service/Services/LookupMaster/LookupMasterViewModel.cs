using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Services.LookupMaster
{
    public class LookupMasterPostModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? SortedOrder { get; set; }
        [Required]
        public string LookUpType { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum 50 char allowed!")]
        public string Value { get; set; }



    }

    public class LookupMasterViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Value { get; set; }
        public int? SortedOrder { get; set; }
        public string? LookUpType { get; set; }
        public string LookUpTypeName { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSubLookup { get; set; }
    }
}

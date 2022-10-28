using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryCategoryViewModel
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string EnumValue { get; set; }
        public string ImagePath { get; set; }
        public bool IsShowInMain { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }
        public int? SortedOrder { get; set; }
        public bool IsSystemEntry { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        


    }
    public class GeneralEntryCategoryPostModel
    {

        public long Id { get; set; }
        [Required]
        [Display(Name = "Name")]

        public string Name { get; set; }
        public string EnumValue { get; set; }
        [Display(Name = "Image")]

        public string ImagePath { get; set; }
        public bool IsShowInMain { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }
        public int? SortedOrder { get; set; }
     
 
    }
}

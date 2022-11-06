using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryPostModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public long CategoryId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "SortedOrder")]
        public int? SortedOrder { get; set; }
        public string ImagePath { get; set; }
        public string Keyword { get; set; }
        public List<string>? Data { get; set; }
    }

    public class GeneralEntryViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string DataId { get; set; }
        public int? SortedOrder { get; set; }
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public List<GeneralEntryDataViewModel>? Data { get; set; }
    }

    public class GeneralEntryDataViewModel
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long? GeneralEntryId { get; set; }
    }

    public class GeneralEntryCategoryViewModel
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string EnumValue { get; set; }
        public string ImagePath { get; set; }

        public int ContentType { get; set; }
        public string ContentTypeText { get; set; }

        public bool IsShowInMain { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }
        public int? SortedOrder { get; set; }
        public bool IsSystemEntry { get; set; }
        public bool IsShowThumbnail { get; set; }
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
        [Required]
        [Display(Name = "ContentType")]
        public int ContentType { get; set; }

        public bool IsShowInMain { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }

        public bool IsShowThumbnail { get; set; }

        public int? SortedOrder { get; set; }


    }
}

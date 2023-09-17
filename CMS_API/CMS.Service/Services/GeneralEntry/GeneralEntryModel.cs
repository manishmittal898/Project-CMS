using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryPostModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public string CategoryId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "SortedOrder")]
        public int? SortedOrder { get; set; }
        public string Url { get; set; }

        public string ImagePath { get; set; }
        public string Keyword { get; set; }
        public List<string>? Data { get; set; }
    }

    public class GeneralEntryViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }

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
        public string Id { get; set; }
        public string Value { get; set; }
        public string? GeneralEntryId { get; set; }
    }

    public class GeneralEntryCategoryViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string EnumValue { get; set; }
        public string ImagePath { get; set; }

        public string ContentType { get; set; }
        public string ContentTypeText { get; set; }
        public bool IsShowUrl { get; set; }
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

        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]

        public string Name { get; set; }

        [Display(Name = "Image")]

        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "ContentType")]
        public string ContentType { get; set; }

        public bool IsShowInMain { get; set; }
        public bool IsShowDataInMain { get; set; }
        public bool IsSingleEntry { get; set; }

        public bool IsShowThumbnail { get; set; }

        public bool IsShowUrl { get; set; }
        public int? SortedOrder { get; set; }


    }
    public class GeneralEntryFilterModel : IndexModel
    {
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string EnumValue { get; set; }

    }
}

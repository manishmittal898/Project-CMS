using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CMS.Service.Services.CMSPage
{
    public class CMSPageViewModel
    {
        public long Id { get; set; }
        public long PageId { get; set; }
        [DataType(DataType.Html)]

        public string Heading { get; set; }
        [DataType(DataType.Html)]
        public string Content { get; set; }
        public int? SortedOrder { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Page { get; set; }
    }

    public class CMSPageListViewModel
    {
        public long PageId { get; set; }
        public string Name { get; set; }
        public int? SortedOrder { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
 
    public class CMSPagePostModel
    {
        public long Id { get; set; }
        [Required]
        public long PageId { get; set; }
                
        [Display(Name = "Heading*")]
        //[StringLength(2000, ErrorMessage = "The {0} char length smaller than {1}.")]
        public string Heading { get; set; }
       
        [Required]
        [Display(Name = "Content*")]
        //[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]

        public string Content { get; set; }
        [Required]
        [Display(Name = "SortedOrder")]
        public int? SortedOrder { get; set; }

    }
}

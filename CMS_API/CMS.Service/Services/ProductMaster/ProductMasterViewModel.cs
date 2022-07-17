using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterPostModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public string Desc { get; set; }
        [Range(0, 9999999999999)]
        public decimal? Price { get; set; }

        [Required]
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CaptionTagId { get; set; }
        public string Summary { get; set; }

        public List<string>? Files { get; set; }
    }

    public class ProductMasterViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string Desc { get; set; }
        public decimal? Price { get; set; }
        public long? CaptionTagId { get; set; }
        public string Summary { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public string CaptionTag { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public List<ProductImageViewModel>? Files { get; set; }

    }
    public class ProductImageViewModel
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public long? ProductId { get; set; }
    }


}

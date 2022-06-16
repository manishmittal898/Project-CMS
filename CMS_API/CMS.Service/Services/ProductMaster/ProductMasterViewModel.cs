using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public string Desc { get; set; }
        [Range(0, 999.99)]
        public decimal? Price { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public string Caption { get; set; }
        public string Summary { get; set; }
        public long CreatedBy { get; set; }

        public long ModifiedBy { get; set; }
    }
}

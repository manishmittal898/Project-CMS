using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductReview
{
  public  class ProductReviewViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public int? Rating { get; set; }
        public int ProductId { get; set; }

        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
    }
}

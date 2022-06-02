﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
  public  class ProductMasterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string Desc { get; set; }
        [Range(0, 999.99)]
        public decimal? Price { get; set; }
        public string Caption { get; set; }
        public string Summary { get; set; }
        public string CreatedBy { get; set; }
       

        public string ModifiedBy { get; set; }
    }
}

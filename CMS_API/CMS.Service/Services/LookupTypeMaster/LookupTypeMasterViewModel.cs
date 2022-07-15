using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupTypeMaster
{
 public  class LookupTypeMasterViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public bool IsImage { get; set; }
        public long CreatedBy { get; set; }
     
        public long ModifiedBy { get; set; }
       
    }
}

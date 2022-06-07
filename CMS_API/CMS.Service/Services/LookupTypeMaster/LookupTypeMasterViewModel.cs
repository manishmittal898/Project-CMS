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
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public string CreatedBy { get; set; }
     
        public string ModifiedBy { get; set; }
       
    }
}

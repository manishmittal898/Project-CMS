using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupMaster
{
  public  class LookupMasterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? SortedOrder { get; set; }
        [Required]
        public int? LookUpType { get; set; }
        public string CreatedBy { get; set; }
     
        public string ModifiedBy { get; set; }
       
      
    }
}

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
        public long  Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? SortedOrder { get; set; }
        [Required]
        public long? LookUpType { get; set; }
        public long CreatedBy { get; set; }
     
        public long ModifiedBy { get; set; }
       
      
    }
}

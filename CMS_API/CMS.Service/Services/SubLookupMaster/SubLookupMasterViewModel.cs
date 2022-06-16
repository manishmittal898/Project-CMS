using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.SubLookupMaster
{
public class SubLookupMasterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int? SortedOrder { get; set; }
        public int LookUpId { get; set; }
      

        public long? CreatedBy { get; set; }
    

        public long? ModifiedBy { get; set; }
    }
}

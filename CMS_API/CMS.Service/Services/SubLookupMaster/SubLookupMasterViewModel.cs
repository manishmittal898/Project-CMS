using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.SubLookupMaster
{
    public class SubLookupMasterPostModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int? SortedOrder { get; set; }
        public string LookUpId { get; set; }

    }
    public class SubLookupMasterViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? SortedOrder { get; set; }
        public string LookUpId { get; set; }
        public string LookUpName { get; set; }

        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

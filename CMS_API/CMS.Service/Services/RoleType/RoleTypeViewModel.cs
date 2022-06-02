using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.RoleType
{
    public class RoleTypeViewModel
    {

        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public int? RoleLevel { get; set; }
        public int? ParentRoleId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
    public class RoleTypePostModel
    {

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? RoleLevel { get; set; }
        public int? ParentRoleId { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

    }
}

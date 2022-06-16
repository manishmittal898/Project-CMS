using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.User
{
    public class UserMasterViewModel
    {
        public long UserId { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]

      //  [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
    
        public string Password { get; set; }
      
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblRoleType Role { get; set; }
        public virtual ICollection<TblProductImage> TblProductImages { get; set; }
        public virtual ICollection<TblProductReview> TblProductReviews { get; set; }
        public virtual ICollection<TblSubLookupMaster> TblSubLookupMasters { get; set; }
    }

    public class UserViewPostModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone Number is required ")]
        //[MaxLength(16),MinLength(10)]
        [MaxLength(16, ErrorMessage = "Phone Number should not be more than 16 digit")]
        [MinLength(9, ErrorMessage = "Phone Number should not be less than 9 digit")]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be number.")]
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }


    }
}

using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.Entities.Account
{
    public class User : BaseEntity
    {
        #region properties
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more that {1} characters")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "Email Active/InActive")]
        public bool IsEmailActive { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(50, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(20, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string MobileActiveCode { get; set; }

        [Display(Name = "Email Active/InActive")]
        public bool IsMobileActive { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} is mandatory")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string LastName { get; set; }

        [Display(Name = "Avatar")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more that {1} characters")]
        public string Avatar { get; set; }

        [Display(Name = "Blocked/Not Blocked")]
        public bool IsBlocked { get; set; }
        #endregion

        #region relations

        #endregion
    }
}

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
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "{0} نام معتبر است")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "ایمیل فعال / غیر فعال")]
        public bool IsEmailActive { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string MobileActiveCode { get; set; }

        [Display(Name = "تلفن همراه فعال/ غیر فعال")]
        public bool IsMobileActive { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "تصویر آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string? Avatar { get; set; }

        [Display(Name = "بلاک شده / بلاک نشدده")]
        public bool IsBlocked { get; set; }
        #endregion

        #region relations

        #endregion
    }
}

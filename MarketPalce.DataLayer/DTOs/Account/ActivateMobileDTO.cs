using MarketPlace.DataLayer.DTOs.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.DTOs.Account
{
    public class ActivateMobileDTO : CaptchaViewModel
    {

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string MobileActiveCode { get; set; }
    }
}

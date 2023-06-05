using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class SiteSetting : BaseEntity
    {
		#region properties

		[Display(Name = "تلفن همراه")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string? Mobile { get; set; }
		[Display(Name = "تلفن")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string? Phone { get; set; }

		[Display(Name = "ایمیل")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		[EmailAddress(ErrorMessage = "{0} نا معتبر است")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[Display(Name = "متن فوتر")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string? FooterText { get; set; }

		[Display(Name = "متن کپی رایت")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string? CopyRight { get; set; }

		[Display(Name = "آدرس")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string? Address { get; set; }

        [Display(Name = "دریاه ما")]
        public string? AboutUs { get; set; }

        [Display(Name = "فعال/ غیرفعال")]
		public bool IsDefault { get; set; }
        #endregion

        #region relations

        #endregion
    }
}

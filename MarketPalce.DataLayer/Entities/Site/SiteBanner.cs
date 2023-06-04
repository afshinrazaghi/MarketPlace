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
	public class SiteBanner : BaseEntity
	{
		#region properties
		[Display(Name = "نام تصویر")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string ImageName { get; set; }
		[Display(Name = "آدرس")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Url { get; set; }

		[Display(Name = "سایز (کلاس های نمایش)")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string GolSize { get; set; }

		[Display(Name = "موقعیت بنر")]
		public BannerPlacement BannerPlacement { get; set; }
		#endregion

		#region relations

		#endregion
	}
}

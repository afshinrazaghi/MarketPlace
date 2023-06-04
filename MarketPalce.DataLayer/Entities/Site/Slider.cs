﻿using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
	public class Slider : BaseEntity
	{
		#region properties

		[Display(Name = "عنوان اصلی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string MainHeader { get; set; }

		[Display(Name = "عنوان فرعی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string SecondHeader { get; set; }

		[Display(Name = "تصویر سلایدر")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string ImageName { get; set; }

		[Display(Name = "توضیحات")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Description { get; set; }

		[Display(Name = "لینک")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Link { get; set; }

		[Display(Name = "متن دکمه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Button { get; set; }

		[Display(Name = "فعال/ غیرعفال")]
		public bool IsActive { get; set; }
		#endregion

		#region relations

		#endregion
	}
}

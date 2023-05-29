using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.DTOs
{
	public class RegisterUserDTO
	{
		[Display(Name = "تلفن همراه")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(50, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Mobile { get; set; }

		[Display(Name = "نام")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string LastName { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		public string Password { get; set; }

		[Display(Name = "تکرار کلمه عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
		[Compare(nameof(Password), ErrorMessage = "کلمه های عبور مغایرت دارند")]
		public string ConfirmPassword { get; set; }
	}

	public enum RegisterUserResult
	{
		Success,
		MobileExists
	}

}

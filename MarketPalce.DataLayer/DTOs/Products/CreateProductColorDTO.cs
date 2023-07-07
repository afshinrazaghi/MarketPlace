using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.DTOs.Products
{
    public class CreateProductColorDTO
    {
        [Display(Name = "رتگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string ColorName { get; set; }

        [Display(Name = "قیمت")]
        [Precision(18, 2)]
        public decimal Price { get; set; }
    }
}

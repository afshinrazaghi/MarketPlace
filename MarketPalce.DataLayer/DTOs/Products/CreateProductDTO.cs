using MarketPlace.DataLayer.Entities.Products;
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
    public class CreateProductDTO
    {
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "قیمت محصول")]
        [Precision(18, 2)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

        public List<CreateProductColorDTO> ProductColors { get; set; }

        [Display(Name = "دسته بندی های محصول")]
        public List<int> ProductCategories { get; set; }

    }

    public enum CreateProductResult
    {
        Success,
        Error
    }


}

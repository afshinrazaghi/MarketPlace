using MarketPlace.DataLayer.Entities.Common;
using MarketPlace.DataLayer.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class Product : BaseEntity
    {
        #region constructor
        public Product()
        {
            ProductSelectedCategories = new HashSet<ProductSelectedCategory>();
            ProductColors = new HashSet<ProductColor>();
        }
        #endregion
        #region properties
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "قیمت محصول")]
        [Precision(18, 2)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }

        [Display(Name = "فروشگاه محصول")]
        public long ProductStoreId { get; set; }
        [Display(Name = "دسته بندی محصول")]
        public long ProductCategoryId { get; set; }
        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "وضعیت")]
        public ProductAcceptanceState ProductAcceptanceState { get; set; }
        [Display(Name = "توضیحات پذیرش/عدم پذیرش محصول")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string? ProductAcceptanceDescription { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string ImageFileName { get; set; }

        #endregion

        #region relations
        [ForeignKey(nameof(ProductCategoryId))]
        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey(nameof(ProductStoreId))]
        public virtual Store ProductStore { get; set; }
        public virtual ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
        #endregion
    }

    public enum ProductAcceptanceState
    {
        [Display(Name = "در حال بررسی")]
        UnderProgress,
        [Display(Name = "تایید شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }
}

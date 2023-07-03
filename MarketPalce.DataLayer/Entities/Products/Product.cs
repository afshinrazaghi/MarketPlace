﻿using MarketPlace.DataLayer.Entities.Common;
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
        public long ProductStoreId { get; set; }
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

        #endregion

        #region relations
        [ForeignKey(nameof(ProductCategoryId))]
        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey(nameof(ProductStoreId))]
        public virtual Store ProductStore { get; set; }
        public virtual ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        #endregion
    }

    public enum ProductAcceptanceState
    {
        UnderProgress,
        Accepted,
        Rejected
    }
}

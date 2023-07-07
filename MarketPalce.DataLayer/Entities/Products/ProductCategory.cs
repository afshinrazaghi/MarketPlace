using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
            ProductSelectedCategories = new HashSet<ProductSelectedCategory>();
            ProductCategoryParentProductCategories = new HashSet<ProductCategory>();
        }
        #region properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string Title { get; set; }


        [Display(Name = "عنوان در Url")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string UrlName { get; set; }


        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

        public long? ParentId { get; set; }
        #endregion

        #region relations
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual ProductCategory? ParentProductCategory { get; set; }

        public virtual ICollection<ProductCategory> ProductCategoryParentProductCategories { get; set; }
        #endregion
    }
}

using MarketPlace.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductColor : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        [Display(Name = "رتگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیش از  {1} کاراکتر باشد")]
        public string ColorName { get; set; }

        [Display(Name = "قیمت")]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        #endregion

        #region relations
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
    }
}

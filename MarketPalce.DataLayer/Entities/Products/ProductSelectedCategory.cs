using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region properties
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }
        #endregion

        #region relations
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }


        [ForeignKey(nameof(ProductCategoryId))]
        public virtual ProductCategory ProductCategory { get; set; }
        #endregion

    }
}

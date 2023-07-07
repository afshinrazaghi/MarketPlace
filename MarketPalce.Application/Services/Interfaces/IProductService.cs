using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region products
        Task<FilterProductDTO> FilterProduct(FilterProductDTO filter);
        #endregion

        #region product categoreis

        Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);
        #endregion
    }
}

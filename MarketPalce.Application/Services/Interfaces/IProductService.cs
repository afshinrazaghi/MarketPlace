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
        Task<CreateProductResult> CreateProduct(CreateProductDTO product, string imageFileName, long storeId);
        #region product categoreis

        Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);
        Task<List<ProductCategory>> GetAllActiveProductCategories();
        #endregion
    }
}

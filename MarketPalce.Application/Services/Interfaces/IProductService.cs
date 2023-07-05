using MarketPlace.DataLayer.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region filter products
        Task<FilterProductDTO> FilterProduct(FilterProductDTO filter);
        #endregion
    }
}

using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region constructor
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        public ProductService(IGenericRepository<Product> productRepository = null, IGenericRepository<ProductCategory> productCategoryRepository = null, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository = null)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
        }
        #endregion


        #region disposable
        public async ValueTask DisposeAsync()
        {
            await _productRepository.DisposeAsync();
            await _productCategoryRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
        } 
        #endregion
    }
}

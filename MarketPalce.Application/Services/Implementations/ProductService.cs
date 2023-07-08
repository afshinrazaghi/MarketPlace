using AutoMapper;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _mapper = mapper;
        }
        #endregion

        #region filter products
        public async Task<FilterProductDTO> FilterProduct(FilterProductDTO filter)
        {
            var query = _productRepository.GetQuery();

            #region state
            switch (filter.FilterProductState)
            {
                case FilterProductState.Active:
                    query = query.Where(p => p.IsActive && p.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.NotActive:
                    query = query.Where(p => !p.IsActive && p.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.Accepted:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Accepted);
                    break;
                case FilterProductState.UnderProgress:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.UnderProgress);
                    break;
                case FilterProductState.Rejected:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Rejected);
                    break;

            }
            #endregion

            #region filter
            if (!string.IsNullOrWhiteSpace(filter.ProductTitle))
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{filter.ProductTitle}%"));

            if (filter.StoreId.HasValue)
                query = query.Where(p => p.ProductStoreId == filter.StoreId.Value);
            #endregion

            var paging = Pager.Build(filter.CurrentPage, await query.CountAsync(), filter.Take, filter.HowManyBeforeAndAfter);
            var products = await query.Paging(paging).ToListAsync();

            filter.SetPaging(paging).SetProducts(products);

            return filter;
        }


        public async Task<CreateProductResult> CreateProduct(CreateProductDTO product, string imageFileName, long storeId)
        {
            //create product
            var newProduct = _mapper.Map<Product>(product);
            newProduct.ImageFileName = imageFileName;
            newProduct.ProductStoreId = storeId;
            await _productRepository.AddEntity(newProduct);
            await _productRepository.SaveChanges();

            //create proudct categories


            //create product colors

            return CreateProductResult.Success;
        }


        #endregion

        #region product categories
        public async Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId)
        {
            return await _productCategoryRepository.GetQuery().Where(productCategory =>
                        productCategory.IsActive && !productCategory.IsDelete && productCategory.ParentId == parentId).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetQuery().Where(pc => pc.IsActive && !pc.IsDelete).ToListAsync();
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

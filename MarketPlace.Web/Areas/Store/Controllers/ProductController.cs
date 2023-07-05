using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.Controllers
{
	public class ProductController : StoreBaseController
	{
		#region constructor
		private readonly IProductService _productService;
		private readonly IStoreService _storeService;
		public ProductController(IProductService productService, IStoreService storeService)
		{
			_productService = productService;
			_storeService = storeService;
		}
		#endregion

		#region list
		[HttpGet("products")]
		public async Task<IActionResult> Index(FilterProductDTO filter)
		{
			var store = await _storeService.GetLastActiveStoreByUserId(User.GetUserId()!.Value);
			if (store != null)
				filter.StoreId = store.Id;

			filter.FilterProductState = FilterProductState.Active;
			var products = await _productService.FilterProduct(filter);
			return View(products);
		}
		#endregion
	}
}

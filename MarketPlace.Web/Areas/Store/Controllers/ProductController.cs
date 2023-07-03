using MarketPlace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.Controllers
{
    public class ProductController : StoreBaseController
    {
        #region constructor
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region list
        public  IActionResult Index()
        {
            return View();
        } 
        #endregion
    }
}

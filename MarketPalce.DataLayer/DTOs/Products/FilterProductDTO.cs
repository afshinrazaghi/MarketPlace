using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Products
{
	public class FilterProductDTO : BasePaging
	{
		#region properties
		public string ProductTitle { get; set; }
		public long? StoreId { get; set; }

        public FilterProductState FilterProductState { get; set; }

        public List<Product> Products { get; set; }
		#endregion

		#region methods

		public FilterProductDTO SetProducts(List<Product> products)
		{
			Products = products;
			return this;
		}

		public FilterProductDTO SetPaging(BasePaging paging)
		{
			CurrentPage = paging.CurrentPage;
			Total = paging.Total;
			PageCount = paging.PageCount;
			Skip = paging.Skip;
			Take = paging.Take;
			HowManyBeforeAndAfter = paging.HowManyBeforeAndAfter;
			First = paging.First;
			Last = paging.Last;
			return this;
		}

		#endregion
	}

	public enum FilterProductState
	{
		UnderProgress,
		Rejected,
		Accepted,
		Active,
		NotActive

	}
}

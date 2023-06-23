using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Entities.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Stores
{
    public class FilterStoreDTO : BasePaging
    {

        #region properties
        public long UserId { get; set; }
        public string? StoreName { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public FilterStoreState StoreState { get; set; }
        public List<Store> Stores { get; set; }
        #endregion

        #region methods
        public FilterStoreDTO SetStores(List<Store> stores)
        {
            Stores = stores;
            return this;
        }

        public FilterStoreDTO SetPaging(BasePaging paging)
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

    public enum FilterStoreState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "در حال بررسی")]
        UnderProgress,
        [Display(Name = "تایید شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }
}

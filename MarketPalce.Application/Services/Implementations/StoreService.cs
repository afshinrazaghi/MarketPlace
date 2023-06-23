﻿using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Stores;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class StoreService : IStoreService
    {
        #region constructor
        private readonly IGenericRepository<Store> _storeService;
        private readonly IGenericRepository<User> _userRepository;
        public StoreService(IGenericRepository<Store> storeService, IGenericRepository<User> userRepository)
        {
            _storeService = storeService;
            _userRepository = userRepository;
        }
        #endregion

        #region store
        public async Task<RequestStoreResult> RequestStore(RequestStoreDTO request, long userId)
        {
            var user = (await _userRepository.GetEntityById(userId))!;
            if (user.IsBlocked) return RequestStoreResult.HasNoPermission;

            bool hasUnderProgressRequest = await _storeService.GetQuery().AnyAsync(store => store.UserId == userId && store.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);
            if (hasUnderProgressRequest) return RequestStoreResult.HasUnderProgressRequest;

            var newStore = new Store()
            {
                Address = request.Address,
                StoreName = request.StoreName,
                UserId = userId,
                Phone = request.Phone,
                StoreAcceptanceState = StoreAcceptanceState.UnderProgress
            };

            await _storeService.AddEntity(newStore);
            await _storeService.SaveChanges();
            return RequestStoreResult.Success;
        }

        public async Task<FilterStoreDTO> FilterStore(FilterStoreDTO filter)
        {
            var query = _storeService.GetQuery().Include(st => st.User)
               .Where(st => !st.IsDelete && st.UserId == filter.UserId);

            #region state
            switch (filter.StoreState)
            {
                case FilterStoreState.All:
                    break;
                case FilterStoreState.UnderProgress:
                    query = query.Where(st => st.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);
                    break;
                case FilterStoreState.Accepted:
                    query = query.Where(st => st.StoreAcceptanceState == StoreAcceptanceState.Accepted);
                    break;
                case FilterStoreState.Rejected:
                    query = query.Where(st => st.StoreAcceptanceState == StoreAcceptanceState.Rejected);
                    break;
            }
            #endregion

            #region filters
            if (!string.IsNullOrEmpty(filter.StoreName))
                query = query.Where(st => EF.Functions.Like(st.StoreName, $"%{filter.StoreName}%"));
            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(st => EF.Functions.Like(st.Phone, $"%{filter.Phone}%"));
            if (!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(st => EF.Functions.Like(st.Mobile, $"%{filter.Mobile}%"));
            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(st => EF.Functions.Like(st.Address, $"%{filter.Address}%"));
            #endregion

            #region pagings
            var basePaging = Pager.Build(filter.CurrentPage, await query.CountAsync(), filter.Take, filter.HowManyBeforeAndAfter);
            var stores = await query.ToListAsync();
            #endregion

            return filter.SetStores(stores).SetPaging(basePaging);
        }
        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _storeService.DisposeAsync();
        }


        #endregion
    }
}

using MarketPlace.Application.Services.Interfaces;
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
        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _storeService.DisposeAsync();
        }
        #endregion
    }
}

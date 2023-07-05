using AutoMapper;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
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
        private readonly IMapper _mapper;
        public StoreService(IGenericRepository<Store> storeService, IGenericRepository<User> userRepository, IMapper mapper)
        {
            _storeService = storeService;
            _userRepository = userRepository;
            _mapper = mapper;
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
            var stores = await query.Paging(basePaging).ToListAsync();
            #endregion

            return filter.SetStores(stores).SetPaging(basePaging);
        }

        public async Task<EditRequestStoreDTO?> GetRequestStoreForEdit(long id, long currentUserId)
        {
            var store = await _storeService.GetEntityById(id);
            if (store == null || store.UserId != currentUserId) return null;
            return _mapper.Map<EditRequestStoreDTO>(store);
        }

        public async Task<EditRequestStoreResult> EditRequestStore(EditRequestStoreDTO storeDTO, long currentUserId)
        {
            var store = await _storeService.GetEntityById(storeDTO.Id);
            if (store == null || store.UserId != currentUserId) return EditRequestStoreResult.NotFound;

            store.StoreName = storeDTO.StoreName;
            store.Phone = storeDTO.Phone;
            store.Address = storeDTO.Address;
            store.StoreAcceptanceState = StoreAcceptanceState.UnderProgress;
            _storeService.EditEntity(store);
            await _storeService.SaveChanges();
            return EditRequestStoreResult.Success;
        }


        public async Task<bool> AcceptStoreRequest(long requestId)
        {
            var request = await _storeService.GetEntityById(requestId);
            if (request != null)
            {
                request.StoreAcceptanceState = StoreAcceptanceState.Accepted;
                _storeService.EditEntity(request);
                await _storeService.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectStoreRequest(RejectItemDTO rejectItem)
        {
            var request = await _storeService.GetEntityById(rejectItem.Id);
            if (request != null)
            {
                request.StoreAcceptanceState = StoreAcceptanceState.Rejected;
                request.StoreAcceptanceDescription = rejectItem.RejectMessage;
                _storeService.EditEntity(request);
                await _storeService.SaveChanges();
                return true;
            }
            return false;
        }

		public async Task<Store?> GetLastActiveStoreByUserId(long userId)
        {
            var store = await  _storeService.GetQuery().OrderByDescending(f=>f.CreateDate).FirstOrDefaultAsync(store=>store.StoreAcceptanceState == StoreAcceptanceState.Accepted && store.UserId == userId);
            return store;
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

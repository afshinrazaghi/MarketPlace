using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.DataLayer.Entities.Stores;
using MarketPlace.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IStoreService : IAsyncDisposable
    {
        #region store
        Task<RequestStoreResult> RequestStore(RequestStoreDTO request, long userId);
        Task<FilterStoreDTO> FilterStore(FilterStoreDTO filter);

        Task<EditRequestStoreDTO?> GetRequestStoreForEdit(long id, long currentUserId);
        Task<EditRequestStoreResult> EditRequestStore(EditRequestStoreDTO store, long currentUserId);
        Task<bool> AcceptStoreRequest(long requestId);
        #endregion
    }
}

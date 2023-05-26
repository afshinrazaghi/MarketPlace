using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        public IQueryable<TEntity> GetAll();
        public Task<TEntity?> GetEntityById(long id);
        public Task AddEntity(TEntity entity);
        void EditEntity(TEntity entity);
        public void DeleteEntity(TEntity entity);
        public Task DeleteEntity(long entityId);
        public void DeletePermanent(TEntity entity);
        public Task DeletePermanent(long entityId);
    }
}

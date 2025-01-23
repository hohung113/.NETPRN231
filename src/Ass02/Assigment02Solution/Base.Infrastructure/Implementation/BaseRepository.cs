using Base.Domain.Interfaces;
using Base.Domain.Interfaces.Domain;
using Base.Share.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Base.Infrastructure.Implementation
{
    public class BaseRepository<TEntity> : BaseCoreQuery<TEntity>, IBaseRepository<TEntity> where TEntity : BaseEntity<Guid>, IEntity
    {
        protected readonly BaseDbContext DbContext; // Only One DbContext can change to  another DbContext to write here
        protected readonly DbSet<TEntity> DbSet;
        public BaseRepository(BaseDbContext dbContext) : base(dbContext, true)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            InitialEntityForCreate(entity);
            DbSet.Add(entity);
            DbContext.SaveChanges();
            return Task.FromResult(entity);
        }

        public Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                InitialEntityForCreate(entity);
            }
            DbSet.AddRange(entities);
            return Task.CompletedTask;
        }


        public async Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        {
            var entities = await DbSet.Where(where).ToListAsync(cancellationToken);
            foreach (var entity in entities)
            {
                if (entity == null) throw new InvalidOperationException("Entity is not found.");

                DbSet.Remove(entity);
            }
        }

        public async Task DeleteAsync(Guid key, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync([key], cancellationToken);
            if (entity == null) throw new InvalidOperationException("Entity is not found.");

            DbSet.Remove(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            foreach (var id in ids)
            {
                var entity = await DbSet.FindAsync([id], cancellationToken);
                if (entity == null) throw new InvalidOperationException("Entity is not found.");

                DbSet.Remove(entity);
            }
        }

        public async Task SoftDeleteAsync(Guid key, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(new object[] { key }, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with key {key} not found.");
            }
            InitialEntityForUpdate(entity); 
            InitialEntityForDelete(entity);
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public Task SoftDeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var trackedEntity = DbContext.Entry(entity);
            InitialEntityForUpdate(entity);
            DbSet.Update(trackedEntity.Adapt<TEntity>());
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

 

        protected virtual void InitialEntityForCreate(TEntity entity)
        {
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();

            if (entity is ICreatedTracking creationTracking)
            {
                creationTracking.CreatedAt = DateTimeOffset.UtcNow;
                creationTracking.CreatedBy = CurrentUserId();
            }
            if (entity is IUpdatedTracking updateTracking)
            {
                updateTracking.UpdatedAt = DateTimeOffset.UtcNow;
                updateTracking.UpdatedBy = CurrentUserId();
            }
        }

        protected virtual void InitialEntityForUpdate(TEntity entity)
        {
            if (entity is IUpdatedTracking updateTracking)
            {
                updateTracking.UpdatedAt = DateTimeOffset.UtcNow;
                updateTracking.UpdatedBy = CurrentUserId();
            }
        }

        protected virtual void InitialEntityForDelete(TEntity entity)
        {
            if (entity is IDeleteTracking updateTracking)
            {
                updateTracking.IsDelete = false;
            }
        }
        private Guid CurrentUserId()
        {
            throw new NotImplementedException();
        }
    }
}

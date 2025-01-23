using Base.Domain.Interfaces;
using Base.Domain.Interfaces.Domain;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;
using Base.Infrastructure.Extension;

namespace Base.Infrastructure.Implementation
{
    public abstract class BaseCoreQuery<TEntity> : IBaseQuery<TEntity> where TEntity : BaseEntity<Guid>, IEntity
    {

        private readonly BaseDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        internal BaseCoreQuery(BaseDbContext dbContext, bool isChangeTrackingEnabled = false)
        {
            _dbContext = dbContext;
            if (isChangeTrackingEnabled)
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            else
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;

            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? where = null,
               CancellationToken cancellationToken = default)
        {
            return await (where == null
                ? _dbSet.CountAsync(cancellationToken)
                : _dbSet.CountAsync(where, cancellationToken));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default)
        {
            return await (where == null
                ? _dbSet.AnyAsync(cancellationToken)
                : _dbSet.AnyAsync(where, cancellationToken));
        }

        #region Get by key throw Exception.

        public async Task<TEntity> GetByKeyAsync(object key, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([key], cancellationToken)
                   ?? throw new Exception($"Entity with ID {key} not found.");
        }

        public async Task<TDto> GetByKeyAsync<TDto>(object key, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync([key], cancellationToken);
            return entity == null ? throw new Exception($"Entity with ID {key} not found.") : entity.Adapt<TDto>();
        }

        public async Task<TDto> GetByKeyAsync<TDto>(object key, Expression<Func<TEntity, TDto>> propertySelectors,
            CancellationToken cancellationToken = default)
        {
            var id = Guid.Parse(Convert.ToString(key) ?? throw new InvalidOperationException());
            return await _dbSet.Where(e => e.Id == id).Select(propertySelectors).FirstOrDefaultAsync(cancellationToken)
                   ?? throw new Exception($"Entity with ID {key} not found.");
        }

        #endregion
        #region Find by key return null.

        public async Task<TEntity?> FindByKeyAsync(object key, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([key], cancellationToken);
        }

        public async Task<TDto?> FindByKeyAsync<TDto>(object key, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync([key], cancellationToken);
            return entity == null ? default : entity.Adapt<TDto>();
        }

        public async Task<TDto?> FindByKeyAsync<TDto>(object key, Expression<Func<TEntity, TDto>> propertySelectors,
            CancellationToken cancellationToken = default)
        {
            var id = Guid.Parse(Convert.ToString(key) ?? throw new InvalidOperationException());
            return await _dbSet.Where(e => e.Id == id).Select(propertySelectors).FirstAsync(cancellationToken);
        }

        #endregion

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(where, cancellationToken);
        }

        public async Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.Where(where).FirstOrDefaultAsync(cancellationToken);
            return entity == null ? default : entity.Adapt<TDto>();
        }

        public async Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.Select(propertySelectors).Where(where).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(where, cancellationToken).ToListAsync(cancellationToken);
        }

        public async Task<List<TDto>> ListAsync<TDto>(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable(where).ToListAsync(cancellationToken);
            return entities.Select(e => e.Adapt<TDto>()).ToList();
        }

        public async Task<List<TDto>> ListAsync<TDto>(string sorting, Expression<Func<TEntity, bool>>? where = null,
         CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable(where).OrderBy(sorting).ToListAsync(cancellationToken);
            return entities.Select(e => e.Adapt<TDto>()).ToList();
        }

        public async Task<List<TDto>> ListAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            Expression<Func<TEntity, bool>>? where,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(where, cancellationToken)
                .Select(propertySelectors)
                .ToListAsync(cancellationToken);
        }

        public async Task<Dictionary<Guid, TEntity>> DicAsync(IEnumerable<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            return (await ListAsync(x => ids.Contains(x.Id), cancellationToken)).ToDictionary(x => x.Id);
        }

        public async Task<(List<TEntity> items, int count)> GetPagedListAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable(where, cancellationToken);
            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);
            return await query.ToPageListAsync(pageIndex, pageSize, count, cancellationToken);
        }

        public async Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? sorting = null, bool count = true,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable(where, cancellationToken).Select(propertySelectors);
            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);
            return await query.ToPageListAsync(pageIndex, pageSize, count, cancellationToken);
        }

        public async Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable(where, cancellationToken);
            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);
            var (items, totalCount) = await query.ToPageListAsync(pageIndex, pageSize, count, cancellationToken);
            return (items.Select(e => e.Adapt<TDto>()).ToList(), totalCount);
        }

        public async Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(IQueryable<TDto> query, int pageIndex, int pageSize,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);
            return await query.ToPageListAsync(pageIndex, pageSize, count, cancellationToken);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (where != null)
                query = query.Where(where);
            return query;
        }
    }
}

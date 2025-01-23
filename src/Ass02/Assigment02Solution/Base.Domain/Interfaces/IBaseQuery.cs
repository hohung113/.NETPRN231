using Base.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces
{
    public interface IBaseQuery<TEntity> : IRepository where TEntity : IEntity
    {
        Task<long> CountAsync(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default);

        #region Get by key throw Exception.

        Task<TEntity> GetByKeyAsync(object key, CancellationToken cancellationToken = default);

        Task<TDto> GetByKeyAsync<TDto>(object key, CancellationToken cancellationToken = default);

        Task<TDto> GetByKeyAsync<TDto>(object key, Expression<Func<TEntity, TDto>> propertySelectors,
            CancellationToken cancellationToken = default);

        #endregion
        #region Find by key return null.

        Task<TEntity?> FindByKeyAsync(object key, CancellationToken cancellationToken = default);

        Task<TDto?> FindByKeyAsync<TDto>(object key, CancellationToken cancellationToken = default);

        Task<TDto?> FindByKeyAsync<TDto>(object key, Expression<Func<TEntity, TDto>> propertySelectors,
            CancellationToken cancellationToken = default);

        #endregion
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default);

        Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default);

        Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            Expression<Func<TEntity, bool>> where,
            CancellationToken cancellationToken = default);

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default);

        Task<List<TDto>> ListAsync<TDto>(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default);

        Task<List<TDto>> ListAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            Expression<Func<TEntity, bool>>? where,
            CancellationToken cancellationToken = default);

        Task<Dictionary<Guid, TEntity>> DicAsync(IEnumerable<Guid> ids,
            CancellationToken cancellationToken = default);

        Task<(List<TEntity> items, int count)> GetPagedListAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default);

        Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(Expression<Func<TEntity, TDto>> propertySelectors,
            int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? sorting = null, bool count = true,
            CancellationToken cancellationToken = default);

        Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default);

        Task<(List<TDto> items, int count)> GetPagedListAsync<TDto>(IQueryable<TDto> query, int pageIndex, int pageSize,
            string? sorting = null, bool count = true, CancellationToken cancellationToken = default);

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default);
    }
}

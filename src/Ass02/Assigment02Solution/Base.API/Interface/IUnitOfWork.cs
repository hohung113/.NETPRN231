using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.API.Interface
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync(CancellationToken cancellationToken = default); 
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}

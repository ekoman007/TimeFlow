using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimeFlow.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {  
        Task<int> Save(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}

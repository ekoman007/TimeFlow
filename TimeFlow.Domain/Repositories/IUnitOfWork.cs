using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    { 
        Task<int> CompleteAsync();
        TRepository GetRepository<TRepository>() where TRepository : class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TimeFlowDbContext _context;
        private readonly IUserRepository _userRepository;
        private bool _disposed;

        public UnitOfWork(TimeFlowDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (typeof(TRepository) == typeof(IUserRepository))
            {
                return _userRepository as TRepository;
            }

            // Add other repositories here as needed
            throw new InvalidOperationException("Repository not found");
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        } 
    }
}

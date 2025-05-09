using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IBussinesProfileRepository : IBusinessProfileRepository
    {
        // This interface inherits from IBusinessProfileRepository for backward compatibility
        Task<bool> GetBussinesProfileByNameAsync(string name, CancellationToken cancellationToken);
    }
} 
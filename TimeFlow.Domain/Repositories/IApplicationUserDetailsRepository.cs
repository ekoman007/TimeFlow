using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IApplicationUserDetailsRepository : IUserDetailsRepository
    {
        // This interface inherits from IUserDetailsRepository for backward compatibility
    }
} 
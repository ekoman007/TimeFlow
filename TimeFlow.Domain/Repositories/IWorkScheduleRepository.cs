using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IWorkScheduleRepository : IRepository<WorkSchedule, int>
    {
        // Add specific methods needed by the application
        Task<IEnumerable<WorkSchedule>> GetByStaffIdAsync(int staffId, CancellationToken cancellationToken = default);
    }
} 
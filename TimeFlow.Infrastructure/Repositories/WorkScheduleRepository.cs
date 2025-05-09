using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class WorkScheduleRepository : GenericRepository<WorkSchedule, int>, IWorkScheduleRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public WorkScheduleRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WorkSchedule>> GetByStaffIdAsync(int staffId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.WorkSchedules
                .Include(ws => ws.WorkShift)
                .Where(ws => ws.StaffId == staffId)
                .ToListAsync(cancellationToken);
        }
    }
} 
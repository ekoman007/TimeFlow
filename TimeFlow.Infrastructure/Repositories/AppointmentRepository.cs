using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment, int>, IAppointmentRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public AppointmentRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<bool> GetAppointmentByNameAsync(int bussinesId, CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments.AnyAsync(u => u.BusinessProfileId == bussinesId, cancellationToken);
        }
    }
}

 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IAppointmentRepository : IRepository<Appointment, int>
    {
        Task<bool> GetAppointmentByNameAsync(int bussinesId, CancellationToken cancellationToken);
    }
}

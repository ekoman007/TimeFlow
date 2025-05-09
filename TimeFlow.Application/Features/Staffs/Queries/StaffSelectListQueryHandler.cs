using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Staffs.Queries
{
    public class StaffSelectListQueryHandler : IRequestHandler<StaffSelectListQuery, List<StaffSelectListModel>>
    {
        private readonly IStaffRepository _staffRepository;

        public StaffSelectListQueryHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


        public async Task<List<StaffSelectListModel>> Handle(StaffSelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var staffList = await _staffRepository
                .GetQueryable(cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return staffList.Select(x => new StaffSelectListModel
            {
                Id = x.Id,
                Name = x.FullName
            }).ToList();
        }
    }
}

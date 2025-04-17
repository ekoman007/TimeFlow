using MediatR; 
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Staffs.Queries
{
    public class StaffByIdQueryHandler : IRequestHandler<StaffByIdQuery, GeneralResponse<StaffModel>>
    {
        private readonly IStaffRepository _staffRepository;

        public StaffByIdQueryHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<GeneralResponse<StaffModel>> Handle(StaffByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var staff = await _staffRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);


            if (staff == null)
            {
                return new GeneralResponse<StaffModel>
                {
                    Success = false,
                    Message = "Staff not found"
                };
            }

            // Kthejeni në modelin e duhur
            var staffModel = new StaffModel
            {
                Id = staff.Id,
                BusinessProfileId = staff.BusinessProfileId,
                FullName = staff.FullName,
                PhoneNumber = staff.PhoneNumber,
                Email = staff.Email,
                RoleId = staff.RoleId

            };

            return new GeneralResponse<StaffModel>
            {
                Success = true,
                Message = "Staff found successfully",
                Result = staffModel
            };
        }
    }
}
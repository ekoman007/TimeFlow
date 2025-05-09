using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Category.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Guests.Commands
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGuestRepository _guestRepository;

        public CreateGuestCommandHandler(IUnitOfWork unitOfWork, IGuestRepository guestRepository)
        {
            _unitOfWork = unitOfWork;
            _guestRepository = guestRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
 

            Guest guest = Guest.Create(request.FullName, request.PhoneNumber, request.Email);


            await _guestRepository.AddAsync(guest, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Guest created successfully.",
                Result = guest.Id
            };
        }
    }
}

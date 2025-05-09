using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.UserDetails.Commands
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UpdateUserDetailsCommandHandler(IUnitOfWork unitOfWork, IUserDetailsRepository userDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            //Duhet me i shtu fushat tjera edhe me check nese id nuk ekziston me gjujt error
            var userDetailsExists = await _userDetailsRepository.GetByUserIdAsync(request.Id, cancellationToken: cancellationToken);

            userDetailsExists.ChangeFullName(request.FullName);
            userDetailsExists.ChangePhoneNumber(request.PhoneNumber); 
            userDetailsExists.ChangeDateOfBirth(request.DateOfBirth);
            userDetailsExists.ChangeProfilePicture(request.ProfilePicture);

            await _userDetailsRepository.UpdateAsync(userDetailsExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "User details updated successfully.",
                Result = userDetailsExists.Id
            };
        }
    }
}

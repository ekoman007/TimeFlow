using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.Roles.Queries;
using TimeFlow.Application.Features.UserDetails.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.UserDetails.Queries
{
    public class UserDetailsByIdQueryHandler : IRequestHandler<UserDetailsByIdQuery, GeneralResponse<UserDetailsModel>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsByIdQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<GeneralResponse<UserDetailsModel>> Handle(UserDetailsByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var userDetails = await _userDetailsRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);


            if (userDetails == null)
            {
                return new GeneralResponse<UserDetailsModel>
                {
                    Success = false,
                    Message = "User details not found"
                };
            }

            // Kthejeni në modelin e duhur
            var userModel = new UserDetailsModel
            {
                Id = userDetails.Id,
                FullName = userDetails.FullName,
                Street = userDetails.Street,
                City = userDetails.City,
                Country = userDetails.Country,
                ZipCode = userDetails.ZipCode,
                PhoneNumber = userDetails.PhoneNumber,
                DateOfBirth = userDetails.DateOfBirth,
                UserId = userDetails.UserId,
            };

            return new GeneralResponse<UserDetailsModel>
            {
                Success = true,
                Message = "Role found successfully",
                Result = userModel
            };
        }
    }
}

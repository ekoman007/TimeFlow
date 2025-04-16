using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.Roles.Queries;
using TimeFlow.Application.Features.UserDetails.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.UserDetails.Queries
{
    public class UserDetailsListQueryHandler : IRequestHandler<UserDetailsListQuery, GeneralResponse<IEnumerable<UserDetailsModel>>>
    {
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsListQueryHandler(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<GeneralResponse<IEnumerable<UserDetailsModel>>> Handle(UserDetailsListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<ApplicationUserDetails> queryable = _userDetailsRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var roles = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var readModel = roles.Select(x =>
                new UserDetailsModel
                {
                    Id = x.Id,
                    FullName = x.FullName, 
                    PhoneNumber = x.PhoneNumber,
                    DateOfBirth = x.DateOfBirth,
                    UserId = x.UserId,
                });

            return new GeneralResponse<IEnumerable<UserDetailsModel>>
            {
                Success = true,
                Message = "User details list.",
                Result = readModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}
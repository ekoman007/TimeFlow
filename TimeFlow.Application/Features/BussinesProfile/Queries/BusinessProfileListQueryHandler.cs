using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileListQueryHandler : IRequestHandler<BusinessProfileListQuery, GeneralResponse<IEnumerable<BussinesProfileModel>>>
    {
        private readonly IBussinesProfileRepository _bussinessProfileRepository;

        public BusinessProfileListQueryHandler(IBussinesProfileRepository bussinessProfileRepository)
        {
            _bussinessProfileRepository = bussinessProfileRepository;
        }

        public async Task<GeneralResponse<IEnumerable<BussinesProfileModel>>> Handle(BusinessProfileListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Aggregates.UsersAggregates.BusinessProfile> queryable = _bussinessProfileRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var bussines = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var bussinesProfileModel = bussines.Select(x =>
                new BussinesProfileModel
                {
                    Id = x.Id,
                    BusinessName = x.BusinessName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Website = x.Website,
                    Description = x.Description,
                    LogoUrl = x.LogoUrl,
                    IndustryId = x.IndustryId,
                    UserDetailsId = x.UserDetailsId,
                });

            return new GeneralResponse<IEnumerable<BussinesProfileModel>>
            {
                Success = true,
                Message = "Bussines Profil list.",
                Result = bussinesProfileModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}
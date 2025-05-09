using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileGetByIdQueryHandler : IRequestHandler<BusinessProfileGetByIdQuery, GeneralResponse<BussinesProfileModel>>
    {
        private readonly IBussinesProfileRepository _bussinessProfileRepository;

        public BusinessProfileGetByIdQueryHandler(IBussinesProfileRepository bussinessProfileRepository)
        {
            _bussinessProfileRepository = bussinessProfileRepository;
        }

        public async Task<GeneralResponse<BussinesProfileModel>> Handle(BusinessProfileGetByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var businessProfile = await _bussinessProfileRepository.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);


            if (businessProfile == null)
            {
                return new GeneralResponse<BussinesProfileModel>
                {
                    Success = false,
                    Message = "BusinessProfile not found"
                };
            }

            // Kthejeni në modelin e duhur
            var businessProfileModel = new BussinesProfileModel
            {
                BusinessName = businessProfile.BusinessName,
                Email = businessProfile.Email,
                PhoneNumber = businessProfile.PhoneNumber,
                Website = businessProfile.Website,
                Description = businessProfile.Description,
                LogoUrl = businessProfile.LogoUrl,
                IndustryId = businessProfile.IndustryId, 
                UserDetailsId = businessProfile.UserDetailsId,
            };

            return new GeneralResponse<BussinesProfileModel>
            {
                Success = true,
                Message = "Business Profile found successfully",
                Result = businessProfileModel
            };
        }
    }
}

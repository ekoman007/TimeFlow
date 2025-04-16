using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.Roles.Queries;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Industry.Queris
{
    public class IndustryByIdQueryHandler : IRequestHandler<IndustryByIdQuery, GeneralResponse<IndustryModel>>
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustryByIdQueryHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<GeneralResponse<IndustryModel>> Handle(IndustryByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var industry = await _industryRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);


            if (industry == null)
            {
                return new GeneralResponse<IndustryModel>
                {
                    Success = false,
                    Message = "Industry not found"
                };
            }

            // Kthejeni në modelin e duhur
            var industryModel = new IndustryModel
            {
                Id = industry.Id,
                Name = industry.Name,
                Description = industry.Description,
                Code = industry.Code,
                ModifiedOn = industry.ModifiedOn, 
            };

            return new GeneralResponse<IndustryModel>
            {
                Success = true,
                Message = "Industry found successfully",
                Result = industryModel
            };
        }
    }
}
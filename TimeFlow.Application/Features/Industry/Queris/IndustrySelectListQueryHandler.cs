using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Roles.DTOs; 
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Industry.Queris
{
    public class IndustrySelectListQueryHandler : IRequestHandler<IndustrySelectListQuery, List<IndustrySelectListModel>>
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustrySelectListQueryHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<List<IndustrySelectListModel>> Handle(IndustrySelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var industryList = await _industryRepository
                .GetQueryable(cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return industryList.Select(x => new IndustrySelectListModel
            {
                Id = x.Id,
                Name = x.Name 
            }).ToList();
        }
    }
}


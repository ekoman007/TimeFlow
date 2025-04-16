using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Industry.DTOs; 
using TimeFlow.Application.Responses; 
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Industry.Queris
{
    public class IndustryListQueryHandler : IRequestHandler<IndustryListQuery, GeneralResponse<IEnumerable<IndustryModel>>>
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustryListQueryHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<GeneralResponse<IEnumerable<IndustryModel>>> Handle(IndustryListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Aggregates.UsersAggregates.Industry> queryable = _industryRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var roles = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var readModel = roles.Select(x =>
                new IndustryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Code = x.Code,
                    ModifiedOn = x.ModifiedOn, 
                });

            return new GeneralResponse<IEnumerable<IndustryModel>>
            {
                Success = true,
                Message = "Industry list.",
                Result = readModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}
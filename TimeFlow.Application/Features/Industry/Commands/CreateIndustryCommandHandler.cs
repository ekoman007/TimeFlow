using AutoMapper;
using MediatR; 
using TimeFlow.Application.Responses; 
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class CreateIndustryCommandHandler : IRequestHandler<CreateIndustryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIndustryRepository _industryRepository;

        public CreateIndustryCommandHandler(IUnitOfWork unitOfWork, IIndustryRepository industryRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _industryRepository = industryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateIndustryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var industry = await _industryRepository.GetIndustryByNameAsync(request.Name, cancellationToken);
            if (industry)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"Industry with this {request.Name} already exists."
                };
            }

            Domain.Aggregates.UsersAggregates.Industry industries = Domain.Aggregates.UsersAggregates.Industry.Create(request.Name, request.Description, request.Code);
             

            await _industryRepository.Add(industries, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Industry created successfully.",
                Result = industries.Id
            };
        }
    }
}
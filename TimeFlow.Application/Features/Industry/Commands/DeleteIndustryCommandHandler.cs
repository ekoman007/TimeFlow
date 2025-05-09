using AutoMapper;
using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class DeleteIndustryCommandHandler : IRequestHandler<DeleteIndustryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIndustryRepository _industryRepository;

        public DeleteIndustryCommandHandler(IUnitOfWork unitOfWork, IIndustryRepository industryRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _industryRepository = industryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteIndustryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
             
            var industryExist = await _industryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (industryExist == null)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "This industry not found"
                };
            }

            if (industryExist.IsActive == false)
            {
                industryExist.Activate();
            }
            else
            {
                industryExist.DeActivate();
            }

            await _industryRepository.UpdateAsync(industryExist, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Industry deleted successfully.",
                Result = industryExist.Id
            };
        }
    }
}

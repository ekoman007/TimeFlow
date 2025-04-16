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

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class UpdateIndustryCommandHandler : IRequestHandler<UpdateIndustryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIndustryRepository _industryRepository;

        public UpdateIndustryCommandHandler(IUnitOfWork unitOfWork, IIndustryRepository industryRepository)
        {
            _unitOfWork = unitOfWork;
            _industryRepository = industryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateIndustryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var industryExists = await _industryRepository.GetById(request.Id, cancellationToken: cancellationToken);

            industryExists.ChangeName(request.Name);
            industryExists.ChangeDescription(request.Description);
            industryExists.ChangeCode(request.Code);

            await _industryRepository.Update(industryExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Role updated successfully.",
                Result = industryExists.Id
            };
        }
    }
}
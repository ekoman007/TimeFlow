using MediatR; 
using TimeFlow.Application.Features.Industry.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var categoryExists = await _categoryRepository.GetById(request.Id, cancellationToken: cancellationToken);

            categoryExists.ChangeName(request.Name);
            categoryExists.ChangeDescription(request.Description);
            categoryExists.ChangeCode(request.Code);
            categoryExists.ChangeIndustry(request.IndustryId);

            await _categoryRepository.Update(categoryExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Category updated successfully.",
                Result = categoryExists.Id
            };
        }
    }
}
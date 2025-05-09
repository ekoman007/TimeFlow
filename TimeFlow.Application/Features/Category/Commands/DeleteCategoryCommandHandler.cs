using MediatR;  
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var categoryExisrts = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (categoryExisrts.IsActive == false)
            {
                categoryExisrts.Activate();
            }
            else
            {
                categoryExisrts.DeActivate();
            }


            await _categoryRepository.UpdateAsync(categoryExisrts, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Category updated successfully.",
                Result = categoryExisrts.Id
            };
        }
    }
}

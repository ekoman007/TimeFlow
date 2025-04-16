using AutoMapper;
using MediatR; 
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork; 
            _categoryRepository = categoryRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var category = await _categoryRepository.GetCategoryByNameAsync(request.Name, cancellationToken);
            if (category)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = $"Category with this {request.Name} already exists."
                };
            }

            Domain.Aggregates.UsersAggregates.Category categories = Domain.Aggregates.UsersAggregates.Category.Create(request.Name, request.Description, request.Code, request.IndustryId);


            await _categoryRepository.Add(categories, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Industry created successfully.",
                Result = categories.Id
            };
        }
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.BussinesProfile.DTOs; 
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    internal class BusinessProfileSelectListQueryHandler : IRequestHandler<BusinessProfileSelectListQuery, List<BussinesProfileSelectListModel>>
    {
        private readonly IBussinesProfileRepository _bussinessProfileRepository;

        public BusinessProfileSelectListQueryHandler(IBussinesProfileRepository bussinessProfileRepository)
        {
            _bussinessProfileRepository = bussinessProfileRepository;
        }

        public async Task<List<BussinesProfileSelectListModel>> Handle(BusinessProfileSelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var bussinesProfileList = await _bussinessProfileRepository
                .GetQueryable(cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return bussinesProfileList.Select(x => new BussinesProfileSelectListModel
            {
                Id = x.Id,
                BussinesName = x.BusinessName
            }).ToList();
        }
    }
}

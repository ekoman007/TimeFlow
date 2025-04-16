using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceSelectListQueryHandler : IRequestHandler<ServiceSelectListQuery, List<ServiceSelectListModel>>
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceSelectListQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<ServiceSelectListModel>> Handle(ServiceSelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var serviceList = await _serviceRepository
                .Get(cancellationToken: cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return serviceList.Select(x => new ServiceSelectListModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
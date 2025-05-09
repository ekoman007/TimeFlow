using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.BusinessAggregates;
using TimeFlow.Domain.Aggregates.Enums;

namespace TimeFlow.Domain.Repositories
{
    public interface IBusinessRepository
    {
        Task<Business> GetByIdAsync(Guid id);
        Task<IEnumerable<Business>> GetAllAsync();
        Task<IEnumerable<Business>> GetByCategoryAsync(BusinessCategory category);
        Task<Business> AddAsync(Business business);
        Task UpdateAsync(Business business);
    }
} 
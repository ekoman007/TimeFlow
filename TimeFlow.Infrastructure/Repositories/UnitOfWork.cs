using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;
using TimeFlow.SharedKernel;

namespace TimeFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TimeFlowDbContext _dbContext;

    public UnitOfWork(TimeFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Save(CancellationToken cancellationToken = default)
    {
        SetDefaultProperties(_dbContext);
        return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void SetDefaultProperties(TimeFlowDbContext dbContext)
    {
        var modifiedItems = dbContext.ChangeTracker
            .Entries<IEntity<int>>()
            .Where(entity => entity.State == EntityState.Modified);

        var newItems = dbContext.ChangeTracker
            .Entries<IEntity<int>>()
            .Where(entity => entity.State == EntityState.Added);

        foreach (var item in modifiedItems)
        {
            item.Entity.SetModifiedOn(DateTime.UtcNow);
        }

        foreach (var item in newItems)
        {
            item.Entity.SetCreatedOn(DateTime.UtcNow);
        }
    }
}
}

﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly TimeFlowDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected GenericRepository(TimeFlowDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(
         Expression<Func<TEntity, bool>> predicate = null,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null,
         CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    queryable = include(queryable);
                }
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            return queryable;
        }


        public async Task<TEntity> GetById(TKey id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if (includes != null && includes.Any())
            {
                foreach (Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include in includes)
                {
                    queryable = include(queryable);
                }
            }

            return await queryable.SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken).ConfigureAwait(false);
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);

            await Task.CompletedTask;
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Delete();
            _dbSet.Update(entity);

            await Task.CompletedTask;
        }

        
    }
}



using Microsoft.EntityFrameworkCore;

namespace TimeFlow.Application.Paged
{
    public static class IQueryablePaginationExtensions
    {
        public static async Task<PagedResult<TDestination>> ToPagedResultAsync<TSource, TDestination>(
            this IQueryable<TSource> query,
            int pageNumber,
            int pageSize,
            Func<TSource, TDestination> selector,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = (await query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync(cancellationToken))
             .Select(selector)
             .ToList();

            return new PagedResult<TDestination>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
        }
    }
}


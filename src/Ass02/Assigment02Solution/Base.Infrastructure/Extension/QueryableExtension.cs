using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Extension
{
    public static class QueryableExtension
    {
        public static async Task<(List<T> items, int count)> ToPageListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize,
    bool count = true, CancellationToken cancellationToken = default)
        {
            var totalCount = 0;
            if (count)
            {
                totalCount = await query.CountAsync(cancellationToken);
                if (totalCount <= 0) return ([], totalCount);
                if (pageIndex * pageSize > totalCount) pageIndex = Convert.ToInt32(Math.Ceiling(totalCount / (double)pageSize));

                if (pageIndex <= 0) pageIndex = 1;
            }

            var list = await query.Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return (list, totalCount);
        }
    }
}

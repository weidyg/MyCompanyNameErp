using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Linq;

namespace MyCompanyName.Erp
{
    public static class IAsyncQueryableExecuterExtensions
    {
        public static async Task<PagedResultDto<T>> ToPageListAsync<T>(this IAsyncQueryableExecuter _asyncExecuter, int page, int pageSize, IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            var totalCount = await _asyncExecuter.CountAsync(queryable);
            var queryList = await _asyncExecuter.ToListAsync(queryable.Page(page, pageSize));
            var resut = new PagedResultDto<T> { TotalCount = totalCount, Items = queryList };
            return resut;
        }
    }
}

using MyCompanyName.Abp.Company;
using MyCompanyName.Identity.Localization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Linq;

namespace MyCompanyName.Identity
{
    public abstract class IdentityAppService : ApplicationService
    {
        protected ICurrentCompany CurrentCompany => LazyServiceProvider.LazyGetRequiredService<ICurrentCompany>();

        protected IdentityAppService()
        {
            LocalizationResource = typeof(IdentityResource);
            ObjectMapperContext = typeof(IdentityApplicationModule);
        }
    }

    public static class IAsyncQueryableExecuterExtensions
    {
        public static async Task<PagedResultDto<T>> ToPageListAsync<T>(
            this IAsyncQueryableExecuter _asyncExecuter,
            int page, int pageSize, IQueryable<T> queryable,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await _asyncExecuter.CountAsync(queryable, cancellationToken);
            var queryList = await _asyncExecuter.ToListAsync(queryable.Page(page, pageSize), cancellationToken);
            var resut = new PagedResultDto<T> { TotalCount = totalCount, Items = queryList };
            return resut;
        }
    }
}

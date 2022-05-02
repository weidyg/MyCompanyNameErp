using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.TenantManagement.EntityFrameworkCore
{
    public static class TenantEfCoreQueryableExtensions
    {
        public static IQueryable<Tenant> IncludeDetails(this IQueryable<Tenant> queryable, bool include = true)
        {
            if (!include) { return queryable; }
            return queryable
                .Include(x => x.ConnectionStrings);
        }
    }
}
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.Identity.EntityFrameworkCore
{
    public class EfCoreCompanyRepository
         : EfCoreRepository<IIdentityDbContext, Company, Guid>,
             ICompanyRepository
    {
        public EfCoreCompanyRepository(
            IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<bool> ExistNameAsync(
         [NotNull] string name,
         CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .AnyAsync(a => a.Name == name,
                GetCancellationToken(cancellationToken));
        }

        public async Task<Company> FindByIdAsync([NotNull] Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
               .IncludeDetails(includeDetails)
               .OrderBy(x => x.Id)
               .FirstOrDefaultAsync(
                   u => u.Id == id,
                   GetCancellationToken(cancellationToken)
               );
        }

        public async Task<Company> FindByNameAsync(
            [NotNull] string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
               .IncludeDetails(includeDetails)
               .OrderBy(x => x.Id)
               .FirstOrDefaultAsync(
                   u => u.Name == name,
                   GetCancellationToken(cancellationToken)
               );
        }

        [Obsolete("Use WithDetailsAsync method.")]
        public override IQueryable<Company> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }

        public override async Task<IQueryable<Company>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
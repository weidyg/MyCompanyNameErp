using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MyCompanyName.Identity
{
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        Task<bool> ExistNameAsync(
         [NotNull] string name,
         CancellationToken cancellationToken = default);

        Task<Company> FindByIdAsync(
         [NotNull] Guid id,
         bool includeDetails = true,
         CancellationToken cancellationToken = default);

        Task<Company> FindByNameAsync(
            [NotNull] string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

    }
}

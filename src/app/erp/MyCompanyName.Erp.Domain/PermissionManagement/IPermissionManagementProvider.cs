using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Erp.Permissions
{
    public interface IPermissionManagementProvider : ISingletonDependency //TODO: Consider to remove this pre-assumption
    {
        string Name { get; }

        Task<PermissionGrantInfo> CheckAsync(
            [NotNull] string name,
            [NotNull] string providerName,
            [NotNull] string providerKey
        );

        Task SetAsync(
            [NotNull] string name,
            [NotNull] string providerKey,
            bool isGranted
        );
    }
}
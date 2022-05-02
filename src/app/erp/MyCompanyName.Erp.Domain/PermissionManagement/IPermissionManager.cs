using JetBrains.Annotations;
using MyCompanyName.Erp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompanyName.Erp.Permissions
{
    public interface IPermissionManager
    {
        Task<PermissionWithGrantedProviders> GetAsync(string permissionName, string providerName, string providerKey);

        Task<List<PermissionWithGrantedProviders>> GetAllAsync([NotNull] string providerName, [NotNull] string providerKey);

        Task SetAsync(string permissionName, string providerName, string providerKey, bool isGranted);

        Task<PermissionGrant> UpdateProviderKeyAsync(PermissionGrant permissionGrant, string providerKey);
    }
}

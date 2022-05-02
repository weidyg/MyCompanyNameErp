using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Erp.Permissions.Identity
{
    public class RolePermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => RolePermissionValueProvider.ProviderName;

        protected IUserFinder UserRoleFinder { get; }

        public RolePermissionManagementProvider(
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUserFinder userRoleFinder)
            : base(
                permissionGrantRepository,
                guidGenerator,
                currentTenant)
        {
            UserRoleFinder = userRoleFinder;
        }

        public async override Task<PermissionGrantInfo> CheckAsync(string name, string providerName, string providerKey)
        {
            if (providerName == Name)
            {
                return new PermissionGrantInfo(
                    await PermissionGrantRepository.FindAsync(name, providerName, providerKey) != null,
                    providerKey
                );
            }

            if (providerName == UserPermissionValueProvider.ProviderName)
            {
                var userId = Guid.Parse(providerKey);
                var roleNames = await UserRoleFinder.GetRolesAsync(userId);
                foreach (var roleName in roleNames)
                {
                    var permissionGrant = await PermissionGrantRepository.FindAsync(name, Name, roleName);
                    if (permissionGrant != null)
                    {
                        return new PermissionGrantInfo(true, roleName);
                    }
                }
            }

            return PermissionGrantInfo.NonGranted;
        }
    }
}

using MyCompanyName.Erp.PermissionManagement;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Erp.Permissions.Identity
{
    public class SystemAdminPermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => SystemAdminPermissionValueProvider.ProviderName;

        protected IUserFinder UserRoleFinder { get; }

        public SystemAdminPermissionManagementProvider(
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
            if (providerName == UserPermissionValueProvider.ProviderName)
            {
                var userId = Guid.Parse(providerKey);
                var isSystemAdmin = await UserRoleFinder.IsSystemAdminAsync(userId);
                if (isSystemAdmin) { return new PermissionGrantInfo(isSystemAdmin, providerKey); }
            }
            return PermissionGrantInfo.NonGranted;
        }
    }
}

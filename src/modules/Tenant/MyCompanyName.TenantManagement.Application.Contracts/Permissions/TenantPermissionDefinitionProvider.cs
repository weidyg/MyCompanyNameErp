using MyCompanyName.TenantManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyCompanyName.TenantManagement.Permissions
{
    public class TenantPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TenantPermissions.GroupName, L("Permission:Tenant"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TenantManagementResource>(name);
        }
    }
}
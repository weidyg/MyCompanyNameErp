using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;

namespace MyCompanyName.Erp.PermissionManagement
{
    public class SystemAdminPermissionValueProvider : PermissionValueProvider
    {
        public SystemAdminPermissionValueProvider(IPermissionStore permissionStore)
            : base(permissionStore)
        {
        }

        public const string ProviderName = "SA";
        public override string Name => ProviderName;

        private bool IsSystemAdmin(ClaimsPrincipal principal) => principal?.FindFirst("User_Type")?.Value == "SystemAdmin";
        public override Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
        {
            var permissionGrant = IsSystemAdmin(context.Principal) ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined;
            return Task.FromResult(permissionGrant);
        }

        public override Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
        {
            var permissionNames = context.Permissions.Select(x => x.Name).Distinct().ToArray();
            var permissionGrant = IsSystemAdmin(context.Principal) ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined;
            return Task.FromResult(new MultiplePermissionGrantResult(permissionNames, permissionGrant));
        }
    }
}

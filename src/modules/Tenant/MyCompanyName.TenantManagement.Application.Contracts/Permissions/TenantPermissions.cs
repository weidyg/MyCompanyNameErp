using Volo.Abp.Reflection;

namespace MyCompanyName.TenantManagement.Permissions
{
    public class TenantPermissions
    {
        public const string GroupName = "Tenant";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(TenantPermissions));
        }
    }
}
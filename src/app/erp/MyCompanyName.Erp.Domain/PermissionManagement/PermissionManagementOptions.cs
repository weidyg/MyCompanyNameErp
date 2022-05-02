using System.Collections.Generic;
using Volo.Abp.Collections;

namespace MyCompanyName.Erp.Permissions
{
    public class PermissionManagementOptions
    {
        public ITypeList<IPermissionManagementProvider> ManagementProviders { get; }

        public Dictionary<string, string> ProviderPolicies { get; }

        public PermissionManagementOptions()
        {
            ManagementProviders = new TypeList<IPermissionManagementProvider>();
            ProviderPolicies = new Dictionary<string, string>();
        }
    }
}

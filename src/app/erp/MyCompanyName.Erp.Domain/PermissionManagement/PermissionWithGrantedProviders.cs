using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;

namespace MyCompanyName.Erp.Permissions
{
    public class PermissionWithGrantedProviders
    {
        public string Name { get; }

        public bool IsGranted { get; set; }

        public List<PermissionValueProviderInfo> Providers { get; set; }

        public PermissionWithGrantedProviders([NotNull] string name, bool isGranted)
        {
            Check.NotNull(name, nameof(name));

            Name = name;
            IsGranted = isGranted;

            Providers = new List<PermissionValueProviderInfo>();
        }
    }
}
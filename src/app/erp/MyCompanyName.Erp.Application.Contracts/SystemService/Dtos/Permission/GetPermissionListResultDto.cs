using System.Collections.Generic;

namespace MyCompanyName.Erp.SystemService
{
    public class GetPermissionListResultDto
    {
        public string EntityDisplayName { get; set; }

        public List<PermissionGroupDto> Groups { get; set; }
    }

    public class PermissionGroupDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<PermissionGrantInfoDto> Permissions { get; set; }
    }
    public class PermissionGrantInfoDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ParentName { get; set; }

        public bool IsGranted { get; set; }

        public List<string> AllowedProviders { get; set; }

        public List<ProviderInfoDto> GrantedProviders { get; set; }

        public int Depth { get; set; }
        public bool Inoperable { get; set; }
        public string DisplaySubName { get; set; }
        public string RootName { get; set; }
    }
    public class ProviderInfoDto
    {
        public string ProviderName { get; set; }

        public string ProviderKey { get; set; }
    }
}
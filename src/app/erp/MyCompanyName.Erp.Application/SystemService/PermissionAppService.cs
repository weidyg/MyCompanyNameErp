using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MyCompanyName.Erp.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Erp.SystemService
{
    [Authorize]
    public class PermissionAppService : ErpAppService, IPermissionAppService
    {
        protected PermissionManagementOptions Options { get; }

        protected IPermissionManager PermissionManager { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }

        public PermissionAppService(
            IOptions<PermissionManagementOptions> options,
            IPermissionManager permissionManager,
            IPermissionDefinitionManager permissionDefinitionManager)
        {
            Options = options.Value;
            PermissionManager = permissionManager;
            PermissionDefinitionManager = permissionDefinitionManager;
        }

        public virtual async Task<GetPermissionListResultDto> GetAsync(string providerName, string providerKey)
        {
            await CheckProviderPolicy(providerName);
            var result = new GetPermissionListResultDto
            {
                EntityDisplayName = providerKey,
                Groups = new List<PermissionGroupDto>()
            };
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();
            foreach (var group in PermissionDefinitionManager.GetGroups())
            {
                var groupDto = new PermissionGroupDto
                {
                    Name = group.Name,
                    DisplayName = group.DisplayName.Localize(StringLocalizerFactory),
                    Permissions = new List<PermissionGrantInfoDto>()
                };
                foreach (var permission in group.GetPermissionsWithChildren())
                {
                    if (!permission.IsEnabled) { continue; }
                    if (permission.Providers.Any() && !permission.Providers.Contains(providerName)) { continue; }
                    if (!permission.MultiTenancySide.HasFlag(multiTenancySide)) { continue; }
                    var grantInfoDto = new PermissionGrantInfoDto
                    {
                        Name = permission.Name,
                        DisplayName = permission.DisplayName.Localize(StringLocalizerFactory),
                        ParentName = permission.Parent?.Name,
                        AllowedProviders = permission.Providers,
                        GrantedProviders = new List<ProviderInfoDto>(),
                    };
                    var grantInfo = await PermissionManager.GetAsync(permission.Name, providerName, providerKey);
                    grantInfoDto.IsGranted = grantInfo.IsGranted;
                    foreach (var provider in grantInfo.Providers)
                    {
                        grantInfoDto.GrantedProviders.Add(new ProviderInfoDto
                        {
                            ProviderName = provider.Name,
                            ProviderKey = provider.Key,
                        });
                    }

                    grantInfoDto.Inoperable = grantInfoDto.IsGranted && grantInfoDto.GrantedProviders.All(p => p.ProviderName != providerName);
                    grantInfoDto.DisplaySubName = grantInfoDto.GrantedProviders.Where(p => p.ProviderName != providerName).Select(p => p.ProviderName).JoinAsString(", ");
                    groupDto.Permissions.Add(grantInfoDto);
                }
                if (groupDto.Permissions.Any())
                {
                    SetDepths(groupDto.Permissions, null, null, 0);
                    result.Groups.Add(groupDto);
                }
            }
            return result;
        }

        public virtual async Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
        {
            await CheckProviderPolicy(providerName);
            foreach (var permissionDto in input.Permissions)
            {
                await PermissionManager.SetAsync(permissionDto.Name, providerName, providerKey, permissionDto.IsGranted);
            }
        }

        protected virtual async Task CheckProviderPolicy(string providerName)
        {
            var policyName = Options.ProviderPolicies.GetOrDefault(providerName ?? "");
            if (policyName.IsNullOrEmpty())
            {
                throw new AbpException($"未定义用于获取/设置提供程序权限的策略 '{providerName}'. 用 {nameof(PermissionManagementOptions)} 制定政策.");
            }
            await AuthorizationService.CheckAsync(policyName);
        }

        private static void SetDepths(List<PermissionGrantInfoDto> items, string rootName, string currentParent, int currentDepth)
        {
            foreach (var item in items)
            {
                if (item.ParentName == currentParent)
                {
                    rootName ??= currentParent;
                    item.RootName = rootName;
                    item.Depth = currentDepth;
                    SetDepths(items, item.RootName, item.Name, currentDepth + 1);
                }
            }
        }
    }
}

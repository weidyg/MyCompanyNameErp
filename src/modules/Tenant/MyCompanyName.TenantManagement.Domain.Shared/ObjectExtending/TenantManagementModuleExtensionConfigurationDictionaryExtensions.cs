using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace MyCompanyName.ObjectExtending
{
    public static class TenantManagementModuleExtensionConfigurationDictionaryExtensions
    {
        public static ModuleExtensionConfigurationDictionary ConfigureTenantManagement(
            this ModuleExtensionConfigurationDictionary modules,
            Action<TenantModuleExtensionConfiguration> configureAction)
        {
            return modules.ConfigureModule(
                TenantModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}
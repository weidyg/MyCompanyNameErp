using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace MyCompanyName.ObjectExtending
{
    public class TenantModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public TenantModuleExtensionConfiguration ConfigureTenant(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                TenantModuleExtensionConsts.EntityNames.Tenant,
                configureAction
            );
        }
    }
}
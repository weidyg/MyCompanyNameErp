using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace MyCompanyName.Identity
{
    [DependsOn(
        typeof(IdentityDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class IdentityApplicationContractsModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                //ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                //    IdentityModuleExtensionConsts.ModuleName,
                //    IdentityModuleExtensionConsts.EntityNames.Role,
                //    getApiTypes: new[] { typeof(IdentityRoleDto) },
                //    createApiTypes: new[] { typeof(IdentityRoleCreateDto) },
                //    updateApiTypes: new[] { typeof(IdentityRoleUpdateDto) }
                //);

                //ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                //    IdentityModuleExtensionConsts.ModuleName,
                //    IdentityModuleExtensionConsts.EntityNames.User,
                //    getApiTypes: new[] { typeof(IdentityUserDto) },
                //    createApiTypes: new[] { typeof(IdentityUserCreateDto) },
                //    updateApiTypes: new[] { typeof(IdentityUserUpdateDto) }
                //);
            });
        }

    }
}

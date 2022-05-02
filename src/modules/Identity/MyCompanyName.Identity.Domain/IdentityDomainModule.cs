using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Abp.Security;
using MyCompanyName.ObjectExtending;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;

namespace MyCompanyName.Identity
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(IdentityDomainSharedModule),
        typeof(MyCompanyNameAbpSecurityModule),
        typeof(AbpAutoMapperModule)
    )]
    public class IdentityDomainModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new();
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<IdentityDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<IdentityDomainMappingProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                //options.EtoMappings.Add<IdentityUser, UserEto>(typeof(IdentityDomainModule));
                //options.EtoMappings.Add<IdentityClaimType, IdentityClaimTypeEto>(typeof(IdentityDomainModule));
                options.EtoMappings.Add<IdentityRole, IdentityRoleEto>(typeof(IdentityDomainModule));
                options.EtoMappings.Add<OrganizationUnit, OrganizationUnitEto>(typeof(IdentityDomainModule));

                options.AutoEventSelectors.Add<IdentityUser>();
                options.AutoEventSelectors.Add<IdentityRole>();
            });

            var identityBuilder = context.Services.AddIdentity(options =>
            {
                //options.User.RequireUniqueEmail = true;
            });

            context.Services.AddObjectAccessor(identityBuilder);
            context.Services.ExecutePreConfiguredActions(identityBuilder);

            Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = AbpClaimTypes.UserId;
                options.ClaimsIdentity.UserNameClaimType = AbpClaimTypes.UserName;
                options.ClaimsIdentity.RoleClaimType = AbpClaimTypes.Role;
            });

            context.Services.AddAbpDynamicOptions<IdentityOptions, IdentityOptionsManager>();
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    typeof(IdentityUser)
                );

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.Role,
                    typeof(IdentityRole)
                );

                //ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                //    IdentityModuleExtensionConsts.ModuleName,
                //    IdentityModuleExtensionConsts.EntityNames.ClaimType,
                //    typeof(IdentityClaimType)
                //);

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.OrganizationUnit,
                    typeof(OrganizationUnit)
                );
            });
        }

    }
}

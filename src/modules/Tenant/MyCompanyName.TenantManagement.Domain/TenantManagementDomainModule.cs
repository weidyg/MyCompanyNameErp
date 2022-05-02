using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.ObjectExtending;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;

namespace MyCompanyName.TenantManagement
{
    [DependsOn(
        typeof(AbpMultiTenancyModule),
        typeof(TenantManagementDomainSharedModule),
        typeof(AbpDataModule),
        typeof(AbpDddDomainModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpCachingModule)
    )]
    public class TenantManagementDomainModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<TenantManagementDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<TenantManagementDomainMappingProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Tenant, TenantEto>();
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    TenantModuleExtensionConsts.ModuleName,
                    TenantModuleExtensionConsts.EntityNames.Tenant,
                    typeof(Tenant)
                );
            });
        }

    }
}

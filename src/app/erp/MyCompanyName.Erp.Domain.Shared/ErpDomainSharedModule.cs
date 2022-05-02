using MyCompanyName.Erp.Localization;
using MyCompanyName.Identity;
using MyCompanyName.Identity.Localization;
using MyCompanyName.TenantManagement;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpLocalizationModule),
        typeof(AbpValidationModule),
        typeof(TenantManagementDomainSharedModule),
        typeof(IdentityDomainSharedModule)
        )]
    public class ErpDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ErpDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ErpResource>("zh-Hans")
                    .AddVirtualJson("/Localization/Erp")
                    .AddBaseTypes(typeof(IdentityResource));
                options.DefaultResourceType = typeof(ErpResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("MyCompanyName.Erp", typeof(ErpResource));
            });
        }
    }
}

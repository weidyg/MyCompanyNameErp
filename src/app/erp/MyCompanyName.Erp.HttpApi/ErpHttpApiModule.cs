using Localization.Resources.AbpUi;
using MyCompanyName.Erp.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(ErpApplicationContractsModule)
        )]
    public class ErpHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<ErpResource>().AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}

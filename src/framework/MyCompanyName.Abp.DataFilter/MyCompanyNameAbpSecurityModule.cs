using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace MyCompanyName.Abp.DataFilter
{
    [DependsOn()]
    public class MyCompanyNameCoreDataFilterModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDataFilterOptions>(options =>
            {
                options.DefaultStates[typeof(IMultiClient)] = new DataFilterState(isEnabled: true);
                options.DefaultStates[typeof(IMultiCompany)] = new DataFilterState(isEnabled: true);
            });
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(AbpHttpClientModule),
        typeof(ErpApplicationContractsModule)
    )]
    public class ErpHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ErpApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}

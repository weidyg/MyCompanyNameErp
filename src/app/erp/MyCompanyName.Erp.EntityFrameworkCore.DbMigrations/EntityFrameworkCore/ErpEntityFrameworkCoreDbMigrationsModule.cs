using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Erp.Data;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    [DependsOn(
        typeof(ErpEntityFrameworkCoreModule)
        )]
    public class ErpEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddTransient<IErpDbSchemaMigrator, ErpEntityFrameworkCoreDbSchemaMigrator>();
            context.Services.AddAbpDbContext<ErpMigrationsDbContext>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Erp.Data;
using MyCompanyName.Erp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore
{
    [DependsOn(
         typeof(ErpEntityFrameworkCoreModule)
         )]
    public class ErpEntityFrameworkCoreMainDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddTransient<IErpDbSchemaMigrator, ErpEntityFrameworkCoreMainDbSchemaMigrator>();
            context.Services.AddAbpDbContext<ErpMainMigrationsDbContext>();
        }
    }
}

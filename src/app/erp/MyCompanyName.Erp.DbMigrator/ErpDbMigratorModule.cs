using MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore;
using MyCompanyName.Erp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ErpEntityFrameworkCoreDbMigrationsModule),
        typeof(ErpEntityFrameworkCoreMainDbMigrationsModule)
        )]
    public class ErpDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}

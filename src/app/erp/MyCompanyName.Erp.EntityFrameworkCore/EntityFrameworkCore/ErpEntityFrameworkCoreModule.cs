using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Abp.EntityFrameworkCore;
using MyCompanyName.Erp.Entities;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Identity.EntityFrameworkCore;
using MyCompanyName.TenantManagement.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    [DependsOn(
        typeof(ErpDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(MyCompanyNameAbpEntityFrameworkCoreModule),
        typeof(TenantManagementEntityFrameworkCoreModule),
        typeof(IdentityEntityFrameworkCoreModule)
        )]
    public class ErpEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ErpEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ErpDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<PermissionGrant, EfCorePermissionGrantRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }
    }
}

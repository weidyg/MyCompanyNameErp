using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MyCompanyName.Identity.EntityFrameworkCore
{
    [DependsOn(
        typeof(IdentityDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(MyCompanyNameAbpEntityFrameworkCoreModule)
    )]
    public class IdentityEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<IdentityDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<IdentityUser, EfCoreIdentityUserRepository>();
                options.AddRepository<IdentityRole, EfCoreIdentityRoleRepository>();
                //options.AddRepository<IdentityClaimType, EfCoreIdentityClaimTypeRepository>();
                options.AddRepository<OrganizationUnit, EfCoreOrganizationUnitRepository>();
                options.AddRepository<IdentitySecurityLog, EFCoreIdentitySecurityLogRepository>();
                options.AddRepository<IdentityLinkUser, EfCoreIdentityLinkUserRepository>();
            });
        }
    }
}
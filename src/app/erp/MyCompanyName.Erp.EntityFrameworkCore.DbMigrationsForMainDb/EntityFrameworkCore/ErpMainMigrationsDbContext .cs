using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MyCompanyName.TenantManagement.EntityFrameworkCore;
using MyCompanyName.Identity.EntityFrameworkCore;
using Volo.Abp.Data;

namespace MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore
{
    [ConnectionStringName(ConnectionStringName)]
    public class ErpMainMigrationsDbContext : AbpDbContext<ErpMainMigrationsDbContext>
    {
        public const string ConnectionStringName = "Tenant";
        public ErpMainMigrationsDbContext(DbContextOptions<ErpMainMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIdentity();
            builder.ConfigureTenantManagement();

            builder.UseMySQL();
        }
    }
}
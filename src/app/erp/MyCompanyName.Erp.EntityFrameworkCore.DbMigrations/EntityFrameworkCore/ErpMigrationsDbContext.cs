using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    /*这个DbContext仅用于数据库迁移。
     * 它不用于运行时。有关运行时DbContext，请参见MyCompanyNameErpDbContext。
     * 它是一个统一的模型，包括
     * 所有使用过的模块和应用程序。
     */
    [ConnectionStringName(ConnectionStringName)]
    public class ErpMigrationsDbContext : AbpDbContext<ErpMigrationsDbContext>
    {
        public const string ConnectionStringName = ErpDbProperties.ConnectionStringName;

        public ErpMigrationsDbContext(DbContextOptions<ErpMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureErp();
            builder.UseMySQL();
        }
    }
}
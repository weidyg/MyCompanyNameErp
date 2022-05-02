using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    /*EFCore控制台命令需要这个类（如添加迁移和更新数据库命令）*/
    public class ErpMigrationsDbContextFactory : IDesignTimeDbContextFactory<ErpMigrationsDbContext>
    {
        public ErpMigrationsDbContext CreateDbContext(string[] args)
        {
            ErpEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ErpMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString(
                    ErpMigrationsDbContext.ConnectionStringName),
                    new MySqlServerVersion(new Version(8, 0, 21)));

            return new ErpMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../erp/MyCompanyName.Erp.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}

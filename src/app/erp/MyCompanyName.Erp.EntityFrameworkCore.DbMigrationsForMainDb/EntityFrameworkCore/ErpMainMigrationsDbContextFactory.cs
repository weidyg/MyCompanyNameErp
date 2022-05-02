using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore;
using System;
using System.IO;

public class ErpMainMigrationsDbContextFactory
    : IDesignTimeDbContextFactory<ErpMainMigrationsDbContext>
{
    public ErpMainMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ErpMainMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString(
                ErpMainMigrationsDbContext.ConnectionStringName),
                new MySqlServerVersion(new Version(8, 0, 21)));

        return new ErpMainMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../erp/MyCompanyName.Erp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);
        return builder.Build();
    }
}
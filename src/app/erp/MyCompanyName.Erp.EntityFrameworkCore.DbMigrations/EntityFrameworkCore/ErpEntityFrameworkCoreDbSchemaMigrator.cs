using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Erp.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    [ExposeServices(typeof(IErpDbSchemaMigrator))]
    public class ErpEntityFrameworkCoreDbSchemaMigrator
        : IErpDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public ErpEntityFrameworkCoreDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider
                .GetRequiredService<ErpMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
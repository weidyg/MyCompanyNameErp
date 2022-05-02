using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.Erp.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore
{
    [ExposeServices(typeof(IErpDbSchemaMigrator))]
    public class ErpEntityFrameworkCoreMainDbSchemaMigrator
        : IErpDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public ErpEntityFrameworkCoreMainDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider
                .GetRequiredService<ErpMainMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}

using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Erp.Data
{
    [ExposeServices(typeof(IErpDbSchemaMigrator))]
    public class NullDbSchemaMigrator : IErpDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}

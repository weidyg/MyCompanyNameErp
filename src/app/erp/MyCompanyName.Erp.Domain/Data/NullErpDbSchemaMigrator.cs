using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Sunton.Erp.Data
{
    public class NullErpDbSchemaMigrator : IErpDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
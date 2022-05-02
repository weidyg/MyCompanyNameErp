using System.Threading.Tasks;

namespace MyCompanyName.Erp.Data
{
    public interface IErpDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

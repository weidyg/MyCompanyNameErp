using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace MyCompanyName.TenantManagement.Data
{
    public class TenantDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IGuidGenerator _guidGenerator;
        public TenantDataSeedContributor(
            IGuidGenerator guidGenerator,
            ITenantRepository tenantRepository)
        {
            _guidGenerator = guidGenerator;
            _tenantRepository = tenantRepository;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            if (context?.TenantId == null)
            {
                await CreateTenantAsync();
            }
        }

        private async Task CreateTenantAsync()
        {
            var tenantName = "MyCompanyName";
            var tenant = await _tenantRepository.FindByNameAsync(tenantName);
            if (tenant == null)
            {
                var tenantEto = new Tenant(_guidGenerator.Create(), tenantName);
                await _tenantRepository.InsertAsync(tenantEto);
            }
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MyCompanyName.Identity.Data
{
    public class IdentityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        public IdentityDataSeedContributor(
            ICompanyRepository companyRepository,
            IdentityUserManager userManager,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _userManager = userManager;
            _companyRepository = companyRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            if (context?.TenantId != null)
            {
                using (_currentTenant.Change(context.TenantId))
                {
                    await CreateCompanyAsync();
                }
            }
        }

        private async Task CreateCompanyAsync()
        {
            var companyId = _guidGenerator.Create();
            var companyName = "XXX有限公司";
            var company = await _companyRepository.FindByNameAsync(companyName);
            if (company == null)
            {
                var companyEto = new Company(companyId, companyName);
                var tenantId = _currentTenant.Id ?? Guid.Empty;
                companyEto.AddLinkTenant(_guidGenerator, "ERP", tenantId);
                companyEto.AddLinkTenant(_guidGenerator, "EFX", tenantId);
                await _companyRepository.InsertAsync(companyEto);
            }
            else
            {
                companyId = company.Id;
            }

            var userName = "admin";
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var userEto = new IdentityUser(_guidGenerator.Create(), userName, companyId);
                await _userManager.CreateAsync(userEto, "1q2w3E*");
                await _userManager.SetLockoutEnabledAsync(userEto, true);
                userEto.Name = "三";
                userEto.Surname = "张";
                userEto.SetSystemAdmin(_guidGenerator);
                await _userManager.UpdateAsync(userEto);
            }
        }
    }
}

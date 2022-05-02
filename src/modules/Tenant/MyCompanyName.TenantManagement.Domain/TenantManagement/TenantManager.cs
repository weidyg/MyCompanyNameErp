using Microsoft.Extensions.Localization;
using MyCompanyName.TenantManagement.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MyCompanyName.TenantManagement
{
    public class TenantManager : DomainService, ITenantManager
    {
        protected ITenantRepository TenantRepository { get; }
        private readonly IStringLocalizer<TenantManagementResource> L;

        public TenantManager(
            IStringLocalizer<TenantManagementResource> localizer,
            ITenantRepository tenantRepository)
        {
            L = localizer;
            TenantRepository = tenantRepository;
        }

        public virtual async Task<Tenant> CreateAsync(string name)
        {
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name);
            return new Tenant(GuidGenerator.Create(), name);
        }

        public virtual async Task ChangeNameAsync(Tenant tenant, string name)
        {
            Check.NotNull(tenant, nameof(tenant));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, tenant.Id);
            tenant.SetName(name);
        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var tenant = await TenantRepository.FindByNameAsync(name);
            if (tenant != null && tenant.Id != expectedId)
            {
                throw new UserFriendlyException(L["DuplicateTenancyName", name]);
            }
        }
    }
}
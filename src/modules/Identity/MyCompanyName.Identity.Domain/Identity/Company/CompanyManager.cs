using Microsoft.Extensions.Localization;
using Sunton.Identity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Sunton.Identity
{
    public class CompanyManager : DomainService
    {
        protected ICompanyRepository CompanyRepository { get; }
        protected IStringLocalizer<IdentityResource> Localizer { get; }
        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public CompanyManager(
            IStringLocalizer<IdentityResource> localizer,
            ICompanyRepository companyRepository,
            ICancellationTokenProvider cancellationTokenProvider)
        {
            Localizer = localizer;
            CompanyRepository = companyRepository;
            CancellationTokenProvider = cancellationTokenProvider;
        }


        //public async Task<string> dsdAsync(Guid companyId)
        //{
        //    var clientType = "";

        //    var company = await CompanyRepository.FindByIdAsync(companyId);
        //    identity.AddClaimIfNotContains(IdentityClaimTypes.CompanyId, company.Id.ToString());
        //    identity.AddClaimIfNotContains(IdentityClaimTypes.CompanyName, company.Name);

        //    identity.AddClaimIfNotContains(IdentityClaimTypes.ClientType, clientType);
        //    var linkTenants = company.FindLinkTenants(clientType);
        //    var tenantId = linkTenants.FirstOrDefault()?.TenantId;
        //    identity.AddClaimIfNotContains(AbpClaimTypes.TenantId, tenantId?.ToString());
        //}
    }
}

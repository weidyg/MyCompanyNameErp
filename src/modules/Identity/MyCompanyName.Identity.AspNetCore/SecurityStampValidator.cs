using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MyCompanyName.Identity.AspNetCore
{
    public class IdentitySecurityStampValidator : SecurityStampValidator<IdentityUser>
    {
        protected ITenantConfigurationProvider TenantConfigurationProvider { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public IdentitySecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager<IdentityUser> signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory,
            ITenantConfigurationProvider tenantConfigurationProvider,
            ICurrentTenant currentTenant)
            : base(
                options,
                signInManager,
                systemClock,
                loggerFactory)
        {
            TenantConfigurationProvider = tenantConfigurationProvider;
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public override async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            var tenant = await TenantConfigurationProvider.GetAsync(saveResolveResult: false);
            using (CurrentTenant.Change(tenant?.Id, tenant?.Name))
            {
                await base.ValidateAsync(context);
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;

namespace MyCompanyName.Identity
{
    public class AbpUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>,
        ITransientDependency
    {
        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
        protected IAbpClaimsPrincipalFactory AbpClaimsPrincipalFactory { get; }

        public AbpUserClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IAbpClaimsPrincipalFactory abpClaimsPrincipalFactory)
            : base(
                userManager,
                roleManager,
                options)
        {
            CurrentPrincipalAccessor = currentPrincipalAccessor;
            AbpClaimsPrincipalFactory = abpClaimsPrincipalFactory;
        }



        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = principal.Identities.First();
            identity.AddClaimIfNotContains(AbpClaimTypes.UserId, user.Id.ToString());
            identity.AddClaimIfNotContains(AbpClaimTypes.Name, user.Name);
            identity.AddClaimIfNotContains(AbpClaimTypes.SurName, user.Surname);
            identity.AddClaimIfNotContains(AbpClaimTypes.PhoneNumber, user.PhoneNumber);
            identity.AddClaimIfNotContains(AbpClaimTypes.Email, user.Email);
            identity.AddClaimIfNotContains(AbpClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString());
            identity.AddClaimIfNotContains(AbpClaimTypes.EmailVerified, user.EmailConfirmed.ToString());
            identity.AddClaims(user?.TempClaims ?? new List<Claim>());
            using (CurrentPrincipalAccessor.Change(identity))
            {
                await AbpClaimsPrincipalFactory.CreateAsync(principal);
            }
            return principal;
        }
    }
}

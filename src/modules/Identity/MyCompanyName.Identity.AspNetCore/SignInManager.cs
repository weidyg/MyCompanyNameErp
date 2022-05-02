using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyCompanyName.Identity.AspNetCore
{
    public class IdentitySignInManager : SignInManager<IdentityUser>
    {
        protected IdentityExternalOptions ExternalOptions { get; }

        public IdentitySignInManager(
            IdentityUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<IdentityUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<IdentityUser> confirmation,
            IOptions<IdentityExternalOptions> options
        ) : base(
            userManager,
            contextAccessor,
            claimsFactory,
            optionsAccessor,
            logger,
            schemes,
            confirmation)
        {
            ExternalOptions = options.Value;
        }

        public async override Task<SignInResult> PasswordSignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool lockoutOnFailure)
        {
            foreach (var externalLoginProviderInfo in ExternalOptions.ExternalLoginProviders.Values)
            {
                var externalLoginProvider = (IExternalLoginProvider)Context.RequestServices
                    .GetRequiredService(externalLoginProviderInfo.Type);
                if (await externalLoginProvider.TryAuthenticateAsync(userName, password))
                {
                    var user = await UserManager.FindByNameAsync(userName);
                    if (user == null) { user = await externalLoginProvider.CreateUserAsync(userName, externalLoginProviderInfo.Name); }
                    else { await externalLoginProvider.UpdateUserAsync(user, externalLoginProviderInfo.Name); }
                    return await SignInOrTwoFactorAsync(user, isPersistent);
                }
            }

            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}

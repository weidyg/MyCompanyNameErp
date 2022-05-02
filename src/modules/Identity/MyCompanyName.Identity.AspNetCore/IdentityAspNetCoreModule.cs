using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MyCompanyName.Identity.AspNetCore
{
    [DependsOn(
        typeof(IdentityDomainModule)
        )]
    public class IdentityAspNetCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IdentityBuilder>(builder =>
            {
                builder
                    .AddDefaultTokenProviders()
                    .AddTokenProvider<LinkUserTokenProvider>(LinkUserTokenProviderConsts.LinkUserTokenProviderName)
                    .AddSignInManager<IdentitySignInManager>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //(TODO: Extract an extension method like IdentityBuilder.AddAbpSecurityStampValidator())
            context.Services.AddScoped<IdentitySecurityStampValidator>();
            context.Services.AddScoped(typeof(SecurityStampValidator<IdentityUser>), provider => provider.GetService(typeof(IdentitySecurityStampValidator)));
            context.Services.AddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(IdentitySecurityStampValidator)));
            var options = context.Services.ExecutePreConfiguredActions(new IdentityAspNetCoreOptions());
            if (options.ConfigureAuthentication)
            {
                context.Services
                    .AddAuthentication(o =>
                    {
                        o.DefaultScheme = IdentityConstants.ApplicationScheme;
                        o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                    })
                    .AddIdentityCookies();
            }
        }
    }

}

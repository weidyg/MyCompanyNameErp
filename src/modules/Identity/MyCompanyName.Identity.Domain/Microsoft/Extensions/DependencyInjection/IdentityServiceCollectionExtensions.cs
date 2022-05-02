using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyCompanyName.Identity;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentity(setupAction: null);
        }

        public static IdentityBuilder AddIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
        {
            //AbpRoleManager
            services.TryAddScoped<IdentityRoleManager>();
            services.TryAddScoped(typeof(RoleManager<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleManager)));

            //AbpUserManager
            services.TryAddScoped<IdentityUserManager>();
            services.TryAddScoped(typeof(UserManager<IdentityUser>), provider => provider.GetService(typeof(IdentityUserManager)));

            //AbpUserStore
            services.TryAddScoped<IdentityUserStore>();
            services.TryAddScoped(typeof(IUserStore<IdentityUser>), provider => provider.GetService(typeof(IdentityUserStore)));

            //AbpRoleStore
            services.TryAddScoped<IdentityRoleStore>();
            services.TryAddScoped(typeof(IRoleStore<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleStore)));

            return services
                .AddIdentityCore<IdentityUser>(setupAction)
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<AbpUserClaimsPrincipalFactory>();
        }
    }
}

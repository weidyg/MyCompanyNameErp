using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyCompanyName.Identity.Settings;
using System;
using System.Threading.Tasks;
using Volo.Abp.Options;
using Volo.Abp.Settings;

namespace MyCompanyName.Identity
{
    public class IdentityOptionsManager : AbpDynamicOptionsManager<IdentityOptions>
    {
        protected ISettingProvider SettingProvider { get; }

        public IdentityOptionsManager(IOptionsFactory<IdentityOptions> factory,
            ISettingProvider settingProvider)
            : base(factory)
        {
            SettingProvider = settingProvider;
        }

        protected override async Task OverrideOptionsAsync(string name, IdentityOptions options)
        {
            options.Password.RequiredLength = await SettingProvider.GetAsync(IdentitySettings.Password.RequiredLength, options.Password.RequiredLength);
            options.Password.RequiredUniqueChars = await SettingProvider.GetAsync(IdentitySettings.Password.RequiredUniqueChars, options.Password.RequiredUniqueChars);
            options.Password.RequireNonAlphanumeric = await SettingProvider.GetAsync(IdentitySettings.Password.RequireNonAlphanumeric, options.Password.RequireNonAlphanumeric);
            options.Password.RequireLowercase = await SettingProvider.GetAsync(IdentitySettings.Password.RequireLowercase, options.Password.RequireLowercase);
            options.Password.RequireUppercase = await SettingProvider.GetAsync(IdentitySettings.Password.RequireUppercase, options.Password.RequireUppercase);
            options.Password.RequireDigit = await SettingProvider.GetAsync(IdentitySettings.Password.RequireDigit, options.Password.RequireDigit);

            options.Lockout.AllowedForNewUsers = await SettingProvider.GetAsync(IdentitySettings.Lockout.AllowedForNewUsers, options.Lockout.AllowedForNewUsers);
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(await SettingProvider.GetAsync(IdentitySettings.Lockout.LockoutDuration, options.Lockout.DefaultLockoutTimeSpan.TotalSeconds.To<int>()));
            options.Lockout.MaxFailedAccessAttempts = await SettingProvider.GetAsync(IdentitySettings.Lockout.MaxFailedAccessAttempts, options.Lockout.MaxFailedAccessAttempts);

            options.SignIn.RequireConfirmedEmail = await SettingProvider.GetAsync(IdentitySettings.SignIn.RequireConfirmedEmail, options.SignIn.RequireConfirmedEmail);
            options.SignIn.RequireConfirmedPhoneNumber = await SettingProvider.GetAsync(IdentitySettings.SignIn.RequireConfirmedPhoneNumber, options.SignIn.RequireConfirmedPhoneNumber);
        }
    }
}

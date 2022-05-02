using MyCompanyName.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace MyCompanyName.Identity.Settings
{
    public class IdentitySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(
                    IdentitySettings.Password.RequiredLength,
                    6.ToString(),
                    L("DisplayName:Identity.Password.RequiredLength"),
                    L("Description:Identity.Password.RequiredLength"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Password.RequiredUniqueChars,
                    1.ToString(),
                    L("DisplayName:Identity.Password.RequiredUniqueChars"),
                    L("Description:Identity.Password.RequiredUniqueChars"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Password.RequireNonAlphanumeric,
                    true.ToString(),
                    L("DisplayName:Identity.Password.RequireNonAlphanumeric"),
                    L("Description:Identity.Password.RequireNonAlphanumeric"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Password.RequireLowercase,
                    true.ToString(),
                    L("DisplayName:Identity.Password.RequireLowercase"),
                    L("Description:Identity.Password.RequireLowercase"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Password.RequireUppercase,
                    true.ToString(),
                    L("DisplayName:Identity.Password.RequireUppercase"),
                    L("Description:Identity.Password.RequireUppercase"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Password.RequireDigit,
                    true.ToString(),
                    L("DisplayName:Identity.Password.RequireDigit"),
                    L("Description:Identity.Password.RequireDigit"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Lockout.AllowedForNewUsers,
                    true.ToString(),
                    L("DisplayName:Identity.Lockout.AllowedForNewUsers"),
                    L("Description:Identity.Lockout.AllowedForNewUsers"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Lockout.LockoutDuration,
                    (5 * 60).ToString(),
                    L("DisplayName:Identity.Lockout.LockoutDuration"),
                    L("Description:Identity.Lockout.LockoutDuration"),
                    true),

                new SettingDefinition(
                    IdentitySettings.Lockout.MaxFailedAccessAttempts,
                    5.ToString(),
                    L("DisplayName:Identity.Lockout.MaxFailedAccessAttempts"),
                    L("Description:Identity.Lockout.MaxFailedAccessAttempts"),
                    true),

                new SettingDefinition(
                    IdentitySettings.SignIn.RequireConfirmedEmail,
                    false.ToString(),
                    L("DisplayName:Identity.SignIn.RequireConfirmedEmail"),
                    L("Description:Identity.SignIn.RequireConfirmedEmail"),
                    true),
                new SettingDefinition(
                    IdentitySettings.SignIn.EnablePhoneNumberConfirmation,
                    true.ToString(),
                    L("DisplayName:Identity.SignIn.EnablePhoneNumberConfirmation"),
                    L("Description:Identity.SignIn.EnablePhoneNumberConfirmation"),
                    true),
                new SettingDefinition(
                    IdentitySettings.SignIn.RequireConfirmedPhoneNumber,
                    false.ToString(),
                    L("DisplayName:Identity.SignIn.RequireConfirmedPhoneNumber"),
                    L("Description:Identity.SignIn.RequireConfirmedPhoneNumber"),
                    true),

                new SettingDefinition(
                    IdentitySettings.User.IsUserNameUpdateEnabled,
                    false.ToString(),
                    L("DisplayName:Identity.User.IsUserNameUpdateEnabled"),
                    L("Description:Identity.User.IsUserNameUpdateEnabled"),
                    true),

                new SettingDefinition(
                    IdentitySettings.User.IsEmailUpdateEnabled,
                    true.ToString(),
                    L("DisplayName:Identity.User.IsEmailUpdateEnabled"),
                    L("Description:Identity.User.IsEmailUpdateEnabled"),
                    true),

                new SettingDefinition(
                    IdentitySettings.OrganizationUnit.MaxUserMembershipCount,
                    int.MaxValue.ToString(),
                    L("Identity.OrganizationUnit.MaxUserMembershipCount"),
                    L("Identity.OrganizationUnit.MaxUserMembershipCount"),
                    true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
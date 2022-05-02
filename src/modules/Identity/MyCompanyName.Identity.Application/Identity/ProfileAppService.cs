using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyCompanyName.Identity.Settings;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace MyCompanyName.Identity
{
    [Authorize]
    public class ProfileAppService : IdentityAppService, IProfileAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }

        public ProfileAppService(
            IdentityUserManager userManager,
            IOptions<IdentityOptions> identityOptions)
        {
            UserManager = userManager;
            IdentityOptions = identityOptions;
        }

        public virtual async Task<ProfileDto> GetAsync()
        {
            var currentUser = await UserManager.GetByIdAsync(CurrentUser.GetId());
            await UserManager.AccessFailedAsync(currentUser);
            return ObjectMapper.Map<IdentityUser, ProfileDto>(currentUser);
        }

        public virtual async Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
        {
            await IdentityOptions.SetAsync();
            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());
            if (!string.Equals(user.UserName, input.UserName, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingProvider.IsTrueAsync(IdentitySettings.User.IsUserNameUpdateEnabled))
                {
                    (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();
                }
            }
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingProvider.IsTrueAsync(IdentitySettings.User.IsEmailUpdateEnabled))
                {
                    (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
                }
            }
            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }
            user.Name = input.Name;
            user.Surname = input.Surname;
            input.MapExtraPropertiesTo(user);
            (await UserManager.UpdateAsync(user)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityUser, ProfileDto>(user);
        }

        public virtual async Task ChangePasswordAsync(ChangePasswordInput input)
        {
            await IdentityOptions.SetAsync();
            var currentUser = await UserManager.GetByIdAsync(CurrentUser.GetId());
            if (currentUser.IsExternal) { throw new BusinessException(code: IdentityErrorCodes.ExternalUserPasswordChange); }
            if (currentUser.PasswordHash == null) { (await UserManager.AddPasswordAsync(currentUser, input.NewPassword)).CheckErrors(); return; }
            (await UserManager.ChangePasswordAsync(currentUser, input.CurrentPassword, input.NewPassword)).CheckErrors();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompanyName.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using IdentityUser = MyCompanyName.Identity.IdentityUser;

namespace MyCompanyName.Web.Shared.Pages
{
    [AllowAnonymous]
    public abstract class AccountPageModel : BasePageModel
    {
        public SignInManager<IdentityUser> SignInManager { get; set; }
        public IdentityUserManager UserManager { get; set; }
        public IdentitySecurityLogManager IdentitySecurityLogManager { get; set; }

        protected AccountPageModel()
        {

        }

        protected virtual RedirectResult RedirectSafely(string returnUrl, string returnUrlHash = null)
        {
            return Redirect(GetRedirectUrl(returnUrl, returnUrlHash));
        }

        protected virtual void CheckIdentityErrors(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new UserFriendlyException("Operation failed: " + identityResult.Errors.Select(e => $"[{e.Code}] {e.Description}").JoinAsString(", "));
            }
        }

        protected virtual string GetRedirectUrl(string returnUrl, string returnUrlHash = null)
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!returnUrlHash.IsNullOrWhiteSpace())
            {
                returnUrl += returnUrlHash;
            }
            return returnUrl;
        }

        protected virtual string NormalizeReturnUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty()) { return GetAppHomeUrl(); }
            if (Url.IsLocalUrl(returnUrl)) { return returnUrl; }
            return GetAppHomeUrl();
        }

        protected virtual void CheckCurrentTenant(Guid? tenantId)
        {
            if (CurrentTenant.Id != tenantId)
            {
                throw new ApplicationException($"Current tenant is different than given tenant. CurrentTenant.Id: {CurrentTenant.Id}, given tenantId: {tenantId}");
            }
        }

        protected virtual string GetAppHomeUrl() { return "/"; }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace MyCompanyName.Efx.Web.Controllers
{
    [Area("MyCompanyName")]
    [Route("MyCompanyName/Tenant/[action]")]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class TenantController : AbpController
    {
        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor => LazyServiceProvider.LazyGetRequiredService<ICurrentPrincipalAccessor>();
        protected ITenantStore TenantStore => LazyServiceProvider.LazyGetRequiredService<ITenantStore>();

        protected string ApplicationScheme { get; } = "Identity.Application";

        [HttpGet]
        public async Task<IActionResult> SwitchAsync(Guid? id, string returnUrl = "")
        {
            if (id.HasValue)
            {
                var linkTenantIds = CurrentUser.GetLinkTenantIds();
                if ((CurrentTenant.Id != id) && linkTenantIds.Contains(id.ToString()))
                {
                    var tenant = await TenantStore.FindAsync(id.Value);
                    if (tenant != null)
                    {
                        var principal = CurrentPrincipalAccessor.Principal;
                        var claims = principal.Claims.Where(w => w.Type != AbpClaimTypes.TenantId).ToList();
                        claims.Add(new Claim(AbpClaimTypes.TenantId, id.ToString()));
                        var newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ApplicationScheme));
                        var identity = newPrincipal.Identities.First();
                        identity.AddClaims(claims);
                        CurrentPrincipalAccessor.Change(newPrincipal);
                        await HttpContext.SignOutAsync(ApplicationScheme);
                        await HttpContext.SignInAsync(ApplicationScheme, newPrincipal);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(returnUrl)) { return Redirect(GetRedirectUrl(returnUrl)); }
            return Redirect("~/");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty()) { return "~/"; }
            if (Url.IsLocalUrl(returnUrl)) { return returnUrl; }
            return "~/";
        }
    }
}

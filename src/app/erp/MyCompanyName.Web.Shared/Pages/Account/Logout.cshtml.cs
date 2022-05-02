using Microsoft.AspNetCore.Mvc;
using MyCompanyName.Identity;
using System.Threading.Tasks;

namespace MyCompanyName.Web.Shared.Pages
{
    public class LogoutModel : AccountPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
                {
                    Identity = IdentitySecurityLogIdentityConsts.Identity,
                    Action = IdentitySecurityLogActionConsts.Logout
                });
            }
            catch { }
            await SignInManager.SignOutAsync();
            if (ReturnUrl != null) { return RedirectSafely(ReturnUrl, ReturnUrlHash); }
            return RedirectToPage("/Login");
        }

        public virtual Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }
    }
}

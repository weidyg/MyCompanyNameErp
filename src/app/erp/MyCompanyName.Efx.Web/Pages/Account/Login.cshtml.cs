using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCompanyName.Abp.DataFilter;
using MyCompanyName.Abp.Security;
using MyCompanyName.Identity;
using MyCompanyName.Identity.AspNetCore;
using MyCompanyName.Web.Shared.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;
using Volo.Abp.Validation;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
namespace MyCompanyName.Efx.Web.Pages
{
    public class LoginModel : AccountPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        [BindProperty]
        [FromBody]
        public LoginVo LoginVo { get; set; }

        private readonly IDataFilter _dataFilter;
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        protected IOptions<ClientOptions> ClientOptions { get; }

        protected ICompanyRepository CompanyRepository => LazyServiceProvider.LazyGetRequiredService<ICompanyRepository>();
        public LoginModel(
            IDataFilter dataFilter,
            IOptions<ClientOptions> clientOptions,
            IOptions<IdentityOptions> identityOptions
            )
        {
            _dataFilter = dataFilter;
            ClientOptions = clientOptions;
            IdentityOptions = identityOptions;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                await SignInManager.SignOutAsync();
            }
            return Page();
        }

        [UnitOfWork(false)]
        public async Task<IActionResult> OnPostAsync()
        {
            using (_dataFilter.Disable<IMultiCompany>())
            {
                ValidateModel();
                var result = SignInResult.Failed;
                await ReplaceEmailToUsernameOfInputIfNeeds();
                await IdentityOptions.SetAsync();
                var user = await UserManager.FindByNameAsync(LoginVo.Username);
                if (user == null) { await ChenckSucceeAndAddSecurityLogAsync(result); }

                #region 
                var clientId = LoginVo.ClientId;
                var tenantId = CurrentTenant.Id;
                var clientType = ClientOptions.Value.GetClientTypeString(clientId);
                if (clientType == null) { throw new BusinessException(IdentityErrorCodes.ClientTypeEmpty); }
                var company = user.CompanyId.HasValue ? await CompanyRepository.FindByIdAsync(user.CompanyId.Value, true) : null;
                if (company == null) { throw new BusinessException(IdentityErrorCodes.UserCompanyEmpty); }
                var linkTenants = company.FindLinkTenants(clientType);
                tenantId = linkTenants.Any(a => a.TenantId == tenantId) ? tenantId : linkTenants.FirstOrDefault()?.TenantId;
                if (tenantId == null) { throw new BusinessException(IdentityErrorCodes.NotFindUserTenant); }
                CurrentTenant.Change(tenantId);
                var tempClaims = new List<Claim>();
                tempClaims.AddIfNotContains(AbpClaimTypes.ClientId, clientId);
                tempClaims.AddIfNotContains(AbpClaimTypes.TenantId, tenantId?.ToString());
                tempClaims.AddIfNotContains(IdentityClaimTypes.ClientType, clientType);
                tempClaims.AddIfNotContains(IdentityClaimTypes.CompanyId, company.Id.ToString());
                tempClaims.AddIfNotContains(IdentityClaimTypes.CompanyName, company.Name);
                tempClaims.Add(IdentityClaimTypes.LinkTenantId, linkTenants.Select(s => s.TenantId.ToString()));
                user.TempClaims = tempClaims;
                #endregion

                result = await SignInManager.PasswordSignInAsync(user, LoginVo.Password, LoginVo.RememberMe, user.LockoutEnabled);
                if (result.RequiresTwoFactor) { return await TwoFactorLoginResultAsync(); }
                if (result.IsLockedOut) { throw new UserFriendlyException(L["UserLockedOutMessage"]); }
                if (result.IsNotAllowed) { throw new UserFriendlyException(L["LoginIsNotAllowed"]); }
                await ChenckSucceeAndAddSecurityLogAsync(result);
                var returnUrl = GetRedirectUrl(ReturnUrl, ReturnUrlHash);
                return new JsonResult(returnUrl);
            }
        }

        private async Task ChenckSucceeAndAddSecurityLogAsync(SignInResult result)
        {
            var securityLog = new IdentitySecurityLogContext()
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = result.ToIdentitySecurityLogAction(),
                UserName = LoginVo.Username,
            };
            await IdentitySecurityLogManager.SaveAsync(securityLog);
            if (!result.Succeeded) { throw new UserFriendlyException(L["InvalidUserNameOrPassword"]); }
        }

        private async Task ReplaceEmailToUsernameOfInputIfNeeds()
        {
            if (!ValidationHelper.IsValidEmailAddress(LoginVo.Username)) { return; }
            var userByUsername = await UserManager.FindByNameAsync(LoginVo.Username);
            if (userByUsername != null) { return; }
            var userByEmail = await UserManager.FindByEmailAsync(LoginVo.Username);
            if (userByEmail == null) { return; }
            LoginVo.Username = userByEmail.UserName;
        }

        private Task<JsonResult> TwoFactorLoginResultAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class LoginVo
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ClientId { get; set; } = "Efx_Web";
    }
}

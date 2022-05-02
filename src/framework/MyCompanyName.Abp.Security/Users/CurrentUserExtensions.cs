using System;
using System.Linq;
using Volo.Abp.Security.Claims;

namespace Volo.Abp.Users
{
    public static class CurrentUserExtensions
    {
        public static Guid? GetCompanyId(this ICurrentUser currentUser)
        {
            var companyIdOrNull = currentUser.FindClaim(IdentityClaimTypes.CompanyId);
            if (companyIdOrNull == null || companyIdOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return Guid.Parse(companyIdOrNull.Value);
        }
        public static string GetCompanyName(this ICurrentUser currentUser)
        {
            return currentUser.FindClaim(IdentityClaimTypes.CompanyName)?.Value;
        }

        public static string GetClientType(this ICurrentUser currentUser)
        {
            return currentUser.FindClaim(IdentityClaimTypes.ClientType)?.Value;
        }
        public static string GetUserType(this ICurrentUser currentUser)
        {
            return currentUser.FindClaim(IdentityClaimTypes.UserType)?.Value;
        }
        public static string[] GetLinkTenantIds(this ICurrentUser currentUser)
        {
            var linkTenants= currentUser.FindClaims(IdentityClaimTypes.LinkTenantId);
            return linkTenants.Select(c => c.Value).ToArray();
        }
    }
}
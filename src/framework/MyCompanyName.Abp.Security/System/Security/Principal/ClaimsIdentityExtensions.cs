using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Security.Claims;

namespace System.Security.Principal
{
    public static class ClaimsIdentityExtensions
    {
        public static Guid? FindCompanyId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));
            var companyIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.CompanyId);
            if (companyIdOrNull == null || companyIdOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return Guid.Parse(companyIdOrNull.Value);
        }

        public static Guid? FindCompanyId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            var claimsIdentity = identity as ClaimsIdentity;
            var companyIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.CompanyId);
            if (companyIdOrNull == null || companyIdOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return Guid.Parse(companyIdOrNull.Value);
        }

        public static string FindCompanyName([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));
            var companyNameOrNull = principal.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.CompanyName);
            if (companyNameOrNull == null || companyNameOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return companyNameOrNull.Value;
        }

        public static string FindCompanyName([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            var claimsIdentity = identity as ClaimsIdentity;
            var companyNameOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.CompanyName);
            if (companyNameOrNull == null || companyNameOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return companyNameOrNull.Value;
        }

        public static string FindClientType([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));
            var companyNameOrNull = principal.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.ClientType);
            if (companyNameOrNull == null || companyNameOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return companyNameOrNull.Value;
        }

        public static string FindClientType([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            var claimsIdentity = identity as ClaimsIdentity;
            var companyNameOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == IdentityClaimTypes.ClientType);
            if (companyNameOrNull == null || companyNameOrNull.Value.IsNullOrWhiteSpace()) { return null; }
            return companyNameOrNull.Value;
        }
    }
}

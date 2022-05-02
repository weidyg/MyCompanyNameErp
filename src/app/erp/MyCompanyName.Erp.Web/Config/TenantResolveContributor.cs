using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Text.Formatting;
using Volo.Abp.Users;

namespace MyCompanyName.Erp.Web
{
    public class ErpDomainTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "ErpDomain";
        public override string Name => ContributorName;
        private static readonly string[] ProtocolPrefixes = { "http://", "https://" };
        private readonly string _domainFormat;
        public ErpDomainTenantResolveContributor(string domainFormat)
        {
            _domainFormat = domainFormat.RemovePreFix(ProtocolPrefixes);
        }

        protected override Task<string> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
        {
            if (!httpContext.Request.Host.HasValue) { return Task.FromResult<string>(null); }
            var hostName = httpContext.Request.Host.Value.RemovePreFix(ProtocolPrefixes);
            if (hostName.StartsWith("localhost")) { return Task.FromResult<string>(null); }
            var extractResult = FormattedStringValueExtracter.Extract(hostName, _domainFormat, ignoreCase: true);
            context.Handled = extractResult.IsMatch;
            return Task.FromResult(extractResult.IsMatch ? extractResult.Matches[0].Value : null);
        }
    }
}

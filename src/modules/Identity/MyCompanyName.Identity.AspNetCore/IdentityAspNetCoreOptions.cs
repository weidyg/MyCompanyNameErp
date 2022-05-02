using Microsoft.AspNetCore.Http;

namespace MyCompanyName.Identity.AspNetCore
{
    public class IdentityAspNetCoreOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool ConfigureAuthentication { get; set; } = true;
    }
}

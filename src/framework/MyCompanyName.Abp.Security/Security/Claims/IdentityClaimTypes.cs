namespace Volo.Abp.Security.Claims
{
    public static class IdentityClaimTypes
    {
        /// <summary>
        /// Default: "company_id".
        /// </summary>
        public static string CompanyId { get; set; } = "company_id";

        /// <summary>
        /// Default: "company_name".
        /// </summary>
        public static string CompanyName { get; set; } = "company_name";

        /// <summary>
        /// Default: "client_type".
        /// </summary>
        public static string ClientType { get; set; } = "client_type";

        /// <summary>
        /// Default: "client_type".
        /// </summary>
        public static string UserType { get; set; } = "user_type";

        /// <summary>
        /// 
        /// </summary>
        public static string LinkTenantId { get; set; } = "link_tenant_id";
    }
}

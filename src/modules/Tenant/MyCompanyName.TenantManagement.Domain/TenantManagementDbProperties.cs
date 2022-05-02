namespace MyCompanyName.TenantManagement
{
    public static class TenantManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Tenant_";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Tenant";
    }
}

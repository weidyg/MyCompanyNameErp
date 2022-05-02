namespace MyCompanyName.Identity
{
    public static class IdentityDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Identity_";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Identity";
    }
}

using Volo.Abp.Data;

namespace MyCompanyName.Erp
{
    public static class ErpDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Default";
    }
}

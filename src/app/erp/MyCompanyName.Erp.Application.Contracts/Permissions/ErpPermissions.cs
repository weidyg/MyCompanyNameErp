namespace MyCompanyName.Erp.Permissions
{
    public static class FinancePermissions
    {
        public const string GroupName = "Finance";
        public static class BankCard
        {
            public const string Default = GroupName + "_BankCard";
            public const string Create = Default + "_Create";
            public const string Update = Default + "_Update";
            public const string Delete = Default + "_Delete";
        }
        public static class ThirdPartyPay
        {
            public const string Default = GroupName + "_ThirdPartyPay";
        }
    }

    public static class ResellerPermissions
    {
        public const string GroupName = "Reseller";
        public static class Level
        {
            public const string Default = GroupName + "_Level";
            public const string Create = Default + "_Create";
            public const string Update = Default + "_Update";
            public const string Delete = Default + "_Delete";
        }
        public static class Reseller
        {
            public const string Default = GroupName + "_Reseller";
            public const string Create = Default + "_Create";
            public const string Update = Default + "_Update";
            public const string Delete = Default + "_Delete";
        }
    }

    //public static class SystemPermissions
    //{
    //    public const string GroupName = "System";

    //    public static class Role
    //    {
    //        public const string Default = GroupName + "_Role";
    //        public const string Create = Default + "_Create";
    //        public const string Update = Default + "_Update";
    //        public const string Delete = Default + "_Delete";
    //        public const string ManagePermissions = Default + "_ManagePermissions";
    //    }

    //    public static class User
    //    {
    //        public const string Default = GroupName + "_User";
    //        public const string Create = Default + "_Create";
    //        public const string Update = Default + "_Update";
    //        public const string Delete = Default + "_Delete";
    //        public const string ManagePermissions = Default + "_ManagePermissions";
    //    }
    //}
}

namespace MyCompanyName.Efx.Web
{
    public class EfxMenus
    {
        public const string Prefix = "Menu";
        public const string Home = Prefix + ".Home";
        public const string MyProfile = Prefix + ".MyProfile";
        public const string LogOut = Prefix + ".LogOut";

        public static class Finance
        {
            public const string Root = Prefix + ".Finance";
            public const string BankCard = Root + ".BankCard";
            public const string ThirdPartyPay = Root + ".ThirdPartyPay";
        }

        public static class System
        {
            public const string Root = Prefix + ".System";
            public const string Identity = Root + ".Identity";
            public const string Identity_User = Identity + ".Users";
            public const string Identity_Role = Identity + ".Roles";
        }

    }
}
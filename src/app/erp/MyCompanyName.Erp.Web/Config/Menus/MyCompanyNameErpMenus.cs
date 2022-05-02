namespace MyCompanyName.Erp.Web
{
    public class ErpMenus
    {
        public const string Prefix = "Menu";
        public const string Home = Prefix + ".Home";
        public const string MyProfile = Prefix + ".MyProfile";
        public const string LogOut = Prefix + ".LogOut";

        public static class Product
        {
            public const string Root = Prefix + ".Product";

            public const string Info = Root + ".Info";
            public const string Info_List = Info + ".List";
            public const string Info_Category = Info + ".Category";
            public const string Info_Brand = Info + ".Brand";
        }
        public static class Order
        {
            public const string Root = Prefix + ".Order";
        }
        public static class Warehouse
        {
            public const string Root = Prefix + ".Warehouse";
        }
        public static class Reseller
        {
            public const string Root = Prefix + ".Reseller";
            public const string Info = Root + ".Info";
            public const string Info_Level = Info + ".Level";
            public const string Info_List = Info + ".List";
        }
        public static class Finance
        {
            public const string Root = Prefix + ".Finance";

            public const string Payer = Root + ".Payer";
            public const string Payer_Account = Payer + ".Account";
            public const string Payer_Account_Detail = Payer_Account + ".Detail";
            public const string Payer_TradeDetail = Payer + ".TradeDetail";

            public const string Payee = Root + ".Payee";
            public const string Payee_BankCard = Payee + ".BankCard";
            public const string Payee_ThirdPartyPay = Payee + ".ThirdPartyPay";
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
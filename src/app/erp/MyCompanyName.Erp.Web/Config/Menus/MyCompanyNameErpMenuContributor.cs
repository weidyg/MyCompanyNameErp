using Microsoft.Extensions.Localization;
using MyCompanyName.Erp.Localization;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Identity.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace MyCompanyName.Erp.Web
{
    public class MyCompanyNameErpMenuContributor : IMenuContributor
    {
        public IStringLocalizer L;
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            L = context.GetLocalizer<ErpResource>();
            if (context.Menu.Name == StandardMenus.Main) { await ConfigureMainMenuAsync(context); }
            if (context.Menu.Name == StandardMenus.User) { await ConfigureUserMenuAsync(context); }
        }

        private ApplicationMenuItem ErpMenuItem(
            string name,
            string url = null,
            string requiredPermissionName = null,
            string icon = null,
            int order = 1000,
            object customData = null,
            string target = null, string elementId = null, string cssClass = null)
            => new(name, L[name], url, icon, order, customData, target, elementId, cssClass, requiredPermissionName);

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            #region 商品
            var Product = ErpMenuItem(ErpMenus.Product.Root, icon: "fa fa-product-hunt");
            var ProductItems = new List<ApplicationMenuItem>()
            {
                //ErpMenuItem(ErpMenus.Product.Info)
                //.AddItem(ErpMenuItem(ErpMenus.Product.Info_List, "/Product"))
                //.AddItem(ErpMenuItem(ErpMenus.Product.Info_Category, "/Product/Brand"))
                //.AddItem(ErpMenuItem(ErpMenus.Product.Info_Brand, "/Product/Brand")),
            };
            ProductItems.ForEach(x => Product.AddItem(x));
            context.Menu.AddItem(Product);
            #endregion

            #region 订单
            var Order = ErpMenuItem(ErpMenus.Order.Root, icon: "fa fa-list-alt");
            var OrderItems = new List<ApplicationMenuItem>()
            {

            };
            OrderItems.ForEach(x => Order.AddItem(x));
            context.Menu.AddItem(Order);
            #endregion

            #region 仓储
            var Warehouse = ErpMenuItem(ErpMenus.Warehouse.Root, icon: "fa fa-warehouse");
            var WarehouseItems = new List<ApplicationMenuItem>()
            {

            };
            WarehouseItems.ForEach(x => Warehouse.AddItem(x));
            context.Menu.AddItem(Warehouse);
            #endregion

            #region 分销商
            var Reseller = ErpMenuItem(ErpMenus.Reseller.Root, icon: "fa fa-users");
            var ResellerItems = new List<ApplicationMenuItem>()
            {
                ErpMenuItem(ErpMenus.Reseller.Info)
               .AddItem(ErpMenuItem(ErpMenus.Reseller.Info_Level, "/Reseller/Level",ResellerPermissions.Level.Default))
               .AddItem(ErpMenuItem(ErpMenus.Reseller.Info_List, "/Reseller/List",ResellerPermissions.Reseller.Default))
            };
            ResellerItems.ForEach(x => Reseller.AddItem(x));
            context.Menu.AddItem(Reseller);
            #endregion

            #region 财务
            var Finance = ErpMenuItem(ErpMenus.Finance.Root, icon: "fa fa-yen-sign");
            var FinanceItems = new List<ApplicationMenuItem>()
            {
                //ErpMenuItem(ErpMenus.Finance.Payer)
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_Account, "/Finance/Account"))
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_Account_Detail, "/Finance/Account/Detail"))
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_TradeDetail, "/Finance/TradeDetail")),

                ErpMenuItem(ErpMenus.Finance.Payee)
               .AddItem(ErpMenuItem(ErpMenus.Finance.Payee_BankCard, "/Finance/BankCard",FinancePermissions.BankCard.Default))
               .AddItem(ErpMenuItem(ErpMenus.Finance.Payee_ThirdPartyPay, "/ThirdPartyPay",FinancePermissions.ThirdPartyPay.Default))
            };
            FinanceItems.ForEach(x => Finance.AddItem(x));
            context.Menu.AddItem(Finance);
            #endregion

            #region 系统
            var Setting = ErpMenuItem(ErpMenus.System.Root, icon: "fa fa-cog");
            var SettingItems = new List<ApplicationMenuItem>()
            {
                 ErpMenuItem(ErpMenus.System.Identity)
                .AddItem(ErpMenuItem(ErpMenus.System.Identity_Role, "/Identity/Role",IdentityPermissions.Roles.Default))
                .AddItem(ErpMenuItem(ErpMenus.System.Identity_User, "/Identity/User",IdentityPermissions.Roles.Default))
            };
            SettingItems.ForEach(x => Setting.AddItem(x));
            context.Menu.AddItem(Setting);
            #endregion

            await Task.CompletedTask;
        }

        private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.AddItem(ErpMenuItem(ErpMenus.MyProfile, "/MyProfile", icon: "fa fa-user"));
            context.Menu.AddItem(ErpMenuItem(ErpMenus.LogOut, "/Logout", icon: "fa fa-sign-out", target: "_top"));
            await Task.CompletedTask;
        }
    }
}

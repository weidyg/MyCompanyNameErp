using Microsoft.Extensions.Localization;
using MyCompanyName.Erp.Localization;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Identity.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace MyCompanyName.Efx.Web
{
    internal class MyCompanyNameEfxMenuContributor : IMenuContributor
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
            #region 财务
            var Finance = ErpMenuItem(EfxMenus.Finance.Root, icon: "fa fa-yen-sign");
            var FinanceItems = new List<ApplicationMenuItem>()
            {
                //ErpMenuItem(ErpMenus.Finance.Payer)
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_Account, "/Finance/Account"))
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_Account_Detail, "/Finance/Account/Detail"))
                //.AddItem(ErpMenuItem(ErpMenus.Finance.Payer_TradeDetail, "/Finance/TradeDetail")),

                ErpMenuItem(EfxMenus.Finance.BankCard, "/Finance/BankCard",FinancePermissions.BankCard.Default)
               //.AddItem(ErpMenuItem(EfxMenus.Finance.Payee_ThirdPartyPay, "/ThirdPartyPay",FinancePermissions.ThirdPartyPay.Default))
            };
            FinanceItems.ForEach(x => Finance.AddItem(x));
            context.Menu.AddItem(Finance);
            #endregion

            #region 系统
            var Setting = ErpMenuItem(EfxMenus.System.Root, icon: "fa fa-cog");
            var SettingItems = new List<ApplicationMenuItem>()
            {
              ErpMenuItem(EfxMenus.System.Identity_Role, "/Identity/Role",IdentityPermissions.Roles.Default),
              ErpMenuItem(EfxMenus.System.Identity_User, "/Identity/User",IdentityPermissions.Roles.Default)
            };
            SettingItems.ForEach(x => Setting.AddItem(x));
            context.Menu.AddItem(Setting);
            #endregion

            await Task.CompletedTask;
        }

        private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            context.Menu.AddItem(ErpMenuItem(EfxMenus.MyProfile, "/MyProfile", icon: "fa fa-user"));
            context.Menu.AddItem(ErpMenuItem(EfxMenus.LogOut, "/Logout", icon: "fa fa-sign-out", target: "_top"));
            await Task.CompletedTask;
        }
    }
}

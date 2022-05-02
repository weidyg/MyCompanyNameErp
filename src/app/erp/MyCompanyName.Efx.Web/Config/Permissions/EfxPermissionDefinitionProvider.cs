using MyCompanyName.Erp.Localization;
using MyCompanyName.Erp.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyCompanyName.Efx.Web.Permissions
{
    public class EfxPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var financeGroup = context.AddGroup(FinancePermissions.GroupName, L("Permission:FinanceManagement"));
            var bankCardPermission = financeGroup.AddPermission(FinancePermissions.BankCard.Default, L("Permission:BankCard"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Create, L("Permission:Create"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Update, L("Permission:Edit"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ErpResource>(name);
        }
    }
}

using MyCompanyName.Erp.Localization;
using MyCompanyName.Erp.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyCompanyName.Erp.Web.Permissions
{
    public class ErpPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var financeGroup = context.AddGroup(FinancePermissions.GroupName, L("Permission:FinanceManagement"));
            var bankCardPermission = financeGroup.AddPermission(FinancePermissions.BankCard.Default, L("Permission:BankCard"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Create, L("Permission:Create"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Update, L("Permission:Edit"));
            bankCardPermission.AddChild(FinancePermissions.BankCard.Delete, L("Permission:Delete"));
            var ThirdPartyPayPermission = financeGroup.AddPermission(FinancePermissions.ThirdPartyPay.Default, L("Permission:ThirdPartyPay"));

            //
            var resellerGroup = context.AddGroup(ResellerPermissions.GroupName, L("Permission:ResellerManagement"));
            var levelPermission = resellerGroup.AddPermission(ResellerPermissions.Level.Default, L("Permission:Level"));
            levelPermission.AddChild(ResellerPermissions.Level.Create, L("Permission:Create"));
            levelPermission.AddChild(ResellerPermissions.Level.Update, L("Permission:Edit"));
            levelPermission.AddChild(ResellerPermissions.Level.Delete, L("Permission:Delete"));

            var resellerPermission = resellerGroup.AddPermission(ResellerPermissions.Reseller.Default, L("Permission:Reseller"));

            //
            //var systemGroup = context.AddGroup(SystemPermissions.GroupName, L("Permission:SystemManagement"));

            //var rolesPermission = systemGroup.AddPermission(SystemPermissions.Role.Default, L("Permission:Role"));
            //rolesPermission.AddChild(SystemPermissions.Role.Create, L("Permission:Create"));
            //rolesPermission.AddChild(SystemPermissions.Role.Update, L("Permission:Edit"));
            //rolesPermission.AddChild(SystemPermissions.Role.Delete, L("Permission:Delete"));
            //rolesPermission.AddChild(SystemPermissions.Role.ManagePermissions, L("Permission:ChangePermissions"));

            //var usersPermission = systemGroup.AddPermission(SystemPermissions.User.Default, L("Permission:User"));
            //usersPermission.AddChild(SystemPermissions.User.Create, L("Permission:Create"));
            //usersPermission.AddChild(SystemPermissions.User.Update, L("Permission:Edit"));
            //usersPermission.AddChild(SystemPermissions.User.Delete, L("Permission:Delete"));
            //usersPermission.AddChild(SystemPermissions.User.ManagePermissions, L("Permission:ChangePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ErpResource>(name);
        }
    }
}

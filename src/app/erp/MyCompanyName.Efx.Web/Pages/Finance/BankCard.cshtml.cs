using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Efx.Web.Pages
{
    [Authorize(FinancePermissions.BankCard.Default)]
    public class BankCardModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}

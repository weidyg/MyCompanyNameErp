using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace MyCompanyName.Erp.Web.Components
{
    public class SiderMenuViewComponent : AbpViewComponent
    {
        private readonly IMenuManager _menuManager;

        public SiderMenuViewComponent(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _menuManager.GetAsync(StandardMenus.Main); 
            return View("~/Components/Menu/Default.cshtml", menu);
        }
    }
}

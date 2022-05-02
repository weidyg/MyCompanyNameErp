using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.Web.Shared.Components
{
    public class FormModalViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke(string id)
        {
            var model = new FormModalViewComponentModel { Id = id };
            return View("~/Components/FormModal/Default.cshtml", model);
        }
    }

    public class FormModalViewComponentModel
    {
        public string Id { get; set; }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using Razui.Basic;

namespace MyCompanyName.Web.Shared
{
    /// <summary>
    /// 
    /// </summary>
    [HtmlTargetElement("a-app")]
    public class AppTagHelper : BaseTagHelper<AppTagHelper, AppTagHelperService>
    {
        public AppTagHelper(AppTagHelperService service)
           : base(service)
        {

        }
    }
}

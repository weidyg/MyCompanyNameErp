using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Razui.Basic;
using System.Threading.Tasks;

namespace MyCompanyName.Web.Shared
{
    public class AppTagHelperService : BaseTagHelperService<AppTagHelper>
    {
        protected IStringLocalizer<AbpUiResource> L { get; }

        public AppTagHelperService(IStringLocalizer<AbpUiResource> localizer)
        {
            L = localizer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "div";
            output.Attributes.SetAttribute("v-cloak", "");
            output.PreContent.AppendHtml("<a-config-provider :locale=\"locale\">");
            output.PostContent.AppendHtml("</a-config-provider>");
        }
    }
}
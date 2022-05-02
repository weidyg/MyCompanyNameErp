using Microsoft.AspNetCore.Razor.TagHelpers;
using Razui.Basic;

namespace MyCompanyName.Web.Shared
{
    [HtmlTargetElement(Attributes = "if")]
    public class IfTagHelper : BaseTagHelper
    {
        [HtmlAttributeName("if")]
        public bool? Condition { get; set; }
        public override void Init(TagHelperContext context)
        {
            base.Init(context);
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Condition.HasValue && Condition.Value == false)
            {
                output.SuppressOutput();
            }
        }
    }
}

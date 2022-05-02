using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Razui.Basic;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCompanyName.Web.Shared
{
    [HtmlTargetElement(Attributes = "compress")]
    public class CompressTagHelper : BaseTagHelper
    {
        private static readonly char[] NameSeparator = new[] { ',' };

        [HtmlAttributeName("compress")]
        public bool Compress { get; set; }

        public CompressTagHelper(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        protected IWebHostEnvironment HostingEnvironment { get; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Compress)
            {
                var currentEnvironmentName = HostingEnvironment.EnvironmentName?.Trim();
                if (currentEnvironmentName?.IsIn("Staging,Production".Split(",")) ?? false)
                {
                    var child = await output.GetChildContentAsync();
                    if (!child.IsEmptyOrWhiteSpace)
                    {
                        var childContent = Regex.Replace(child.GetContent().Replace("\r\n", ""), @"\s+", " ");
                        output.Content.SetHtmlContent($"{childContent}");
                    }
                }
            }
        }
    }
}

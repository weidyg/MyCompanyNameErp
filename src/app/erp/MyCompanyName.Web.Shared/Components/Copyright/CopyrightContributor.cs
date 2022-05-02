using Razui.Bundling;
using System.Collections.Generic;

namespace MyCompanyName.Web.Shared.Components
{
    public class CopyrightStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/Copyright/Default.cshtml.css");
        }
    }
}

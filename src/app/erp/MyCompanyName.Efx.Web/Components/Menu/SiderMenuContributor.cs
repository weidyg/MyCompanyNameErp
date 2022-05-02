using Razui.Bundling;
using System.Collections.Generic;

namespace MyCompanyName.Erp.Web.Components
{
    public class SiderMenuStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/Menu/Default.cshtml.css");
        }
    }

    public class SiderMenuScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/Menu/Default.cshtml.js");
        }
    }
}

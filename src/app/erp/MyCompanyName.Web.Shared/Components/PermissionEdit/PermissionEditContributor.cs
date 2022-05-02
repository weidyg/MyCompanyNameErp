using Razui.Bundling;
using System.Collections.Generic;

namespace MyCompanyName.Web.Shared.Components
{
    public class PermissionEditContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/PermissionEdit/Default.cshtml.js");
        }
    }
}

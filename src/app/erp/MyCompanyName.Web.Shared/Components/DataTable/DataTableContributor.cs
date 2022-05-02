using Razui.Bundling;
using System.Collections.Generic;

namespace MyCompanyName.Web.Shared.Components
{
    public class DataTableContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/DataTable/Default.cshtml.js");
        }
    }
}

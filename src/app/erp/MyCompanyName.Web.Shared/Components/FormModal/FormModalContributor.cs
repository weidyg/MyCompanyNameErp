using Razui.Bundling;
using System.Collections.Generic;

namespace MyCompanyName.Web.Shared.Components
{
    public class FormModalContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/FormModal/Default.cshtml.js");
        }
    }
}

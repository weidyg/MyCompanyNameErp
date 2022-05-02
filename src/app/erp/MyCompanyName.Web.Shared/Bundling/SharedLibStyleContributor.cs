using Razui.Bundling;

namespace MyCompanyName.Web.Shared.Bundling
{
    public class SharedLibStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddRange(new[]
            {
                "/libs/fontawesome/css/all.css",
                "/libs/fontawesome/css/v4-shims.css",
                "/libs/ant-design-vue/dist/antd.css",
                "/css/global.css",
                "/css/page.css"
            });
        }
    }
}
using Razui.Bundling;

namespace MyCompanyName.Web.Shared.Bundling
{
    public class SharedLibScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            // antd.js 行 19541  this.$slots.renderItem 改成 this.$slots.renderitem
            // antd.js 行 5914... ["getSlot"])(this, 'dataSource') 改成... ["getSlot"])(this, 'options')
            context.Files.AddRange(new[]
            {
                //"/libs/vue/dist/vue.global.js",
                "/libs/moment.js/moment.js",
                "/libs/ant-design-vue/dist/antd.js",
                "/libs/jquery/jquery.js",
                "/libs/abp/core/abp.js",
                "/libs/abp/jquery/abp.jquery.js",
                "/libs/md5/dist/md5.min.js"
            });
        }
    }
}
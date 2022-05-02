using Microsoft.Extensions.Localization;
using MyCompanyName.Erp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MyCompanyName.Erp.Web
{
    public class BrandingProvider : IBrandingProvider, ITransientDependency
    {
        private readonly IStringLocalizer<ErpResource> L;
        public BrandingProvider(
            IStringLocalizer<ErpResource> localizer)
        {
            L = localizer;
        }
        public virtual string AppName => L["MyCompanyNameEFX"];

        public virtual string LogoUrl => "/images/logo.png";

        public virtual string LogoReverseUrl => null;
    }
}

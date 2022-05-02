using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;

namespace MyCompanyName.Web.Shared.Components
{
    public class LanguageSwitchViewComponent : AbpViewComponent
    {
        private readonly ILanguageProvider _languageProvider;

        public LanguageSwitchViewComponent(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageProvider.GetLanguagesAsync();
            var currentLanguage = languages.FindByCulture(
                CultureInfo.CurrentCulture.Name,
                CultureInfo.CurrentUICulture.Name
            );

            if (currentLanguage == null)
            {
                var abpRequestLocalizationOptionsProvider = HttpContext.RequestServices.GetRequiredService<IAbpRequestLocalizationOptionsProvider>();
                var localizationOptions = await abpRequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
                if (localizationOptions.DefaultRequestCulture != null)
                {
                    currentLanguage = new LanguageInfo(
                        localizationOptions.DefaultRequestCulture.Culture.Name,
                        localizationOptions.DefaultRequestCulture.UICulture.Name,
                        localizationOptions.DefaultRequestCulture.UICulture.DisplayName);
                }
                else
                {
                    currentLanguage = new LanguageInfo(
                        CultureInfo.CurrentCulture.Name,
                        CultureInfo.CurrentUICulture.Name,
                        CultureInfo.CurrentUICulture.DisplayName);
                }
            }

            var model = new LanguageSwitchViewComponentModel
            {
                CurrentLanguage = currentLanguage,
                OtherLanguages = languages.Where(l => l != currentLanguage).ToList()
            };

            return View("~/Components/Toolbar/LanguageSwitch/Default.cshtml", model);
        }
    }

    public class LanguageSwitchViewComponentModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public List<LanguageInfo> OtherLanguages { get; set; }

        public string LanguageChangeHref(LanguageInfo language, string encodedPathQuery)
        {
            return $"/Abp/Languages/Switch?culture={language.CultureName}&uiCulture={language.UiCultureName}&returnUrl={WebUtility.UrlEncode(encodedPathQuery)}";
        }
    }
}

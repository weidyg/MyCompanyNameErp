using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.Web.Shared.Components
{
    public class CopyrightViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke(Copright copright)
        {
            var nsCode = Regex.Match(copright.NsName ?? "", "(?<=公网安备).*?(?=号)").Value?.Trim();
            copright.NsLink ??= $"http://www.beian.gov.cn/portal/registerSystemInfo?recordcode={nsCode}";
            copright.IcpLink ??= "https://beian.miit.gov.cn/";
            return View("~/Components/Copyright/Default.cshtml", copright);
        }
    }
    public class Copright
    {
        public int? Year { get; set; }
        public string ComName { get; set; }
        public string ComLink { get; set; }
        public string IcpName { get; set; }
        public string IcpLink { get; set; }
        public string NsName { get; set; }
        public string NsLink { get; set; }
    }
}

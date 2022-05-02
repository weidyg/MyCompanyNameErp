using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;
using Volo.Abp.Sms.Aliyun;

namespace MyCompanyName.Abp.Sms
{
    [DependsOn(typeof(AbpSmsModule))]
    public class MyCompanyNameAbpSmsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<AbpAliyunSmsOptions>(configuration.GetSection("AbpAliyunSms"));
        }
    }
}

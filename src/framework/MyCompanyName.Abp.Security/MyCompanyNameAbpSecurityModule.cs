using Volo.Abp.Modularity;
using Volo.Abp.Security;

namespace MyCompanyName.Abp.Security
{
    [DependsOn(typeof(AbpSecurityModule))]
    public class MyCompanyNameAbpSecurityModule : AbpModule
    {

    }
}

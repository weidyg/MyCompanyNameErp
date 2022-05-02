using MyCompanyName.Abp.DataFilter;
using MyCompanyName.Abp.Security;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MyCompanyName.Abp.EntityFrameworkCore
{
    [DependsOn(
       typeof(MyCompanyNameCoreDataFilterModule),
       typeof(MyCompanyNameAbpSecurityModule),
       typeof(AbpEntityFrameworkCoreModule)
   )]
    public class MyCompanyNameAbpEntityFrameworkCoreModule : AbpModule
    {

    }
}

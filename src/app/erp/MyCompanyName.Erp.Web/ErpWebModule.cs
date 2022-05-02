using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.Abp.Security;
using MyCompanyName.Web.Shared;
using System.IO;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SecurityLog;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.Erp.Web
{
    [DependsOn(
         typeof(WebSharedModule)
         )]
    public class ErpWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            ConfigureVirtualFileSystem(services);
            ConfigureAutoMapper(services);
            ConfigureMenus();
            ConfigureSecurityLog();
        }

        private void ConfigureSecurityLog()
        {
            Configure<AbpSecurityLogOptions>(options =>
            {
                options.IsEnabled = true;
                options.ApplicationName = ClientType.ERP.ToString();
            });
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapperObjectMapper<ErpWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ErpWebAutoMapperProfile>(validate: true);
            });
        }


        private void ConfigureMenus()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyCompanyNameErpMenuContributor());
            });
        }

        /// <summary>
        /// 虚拟文件系统
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureVirtualFileSystem(IServiceCollection services)
        {
            var hostingEnvironment = services.GetHostingEnvironment();
            if (hostingEnvironment.IsDevelopment())
            {
                string name = "MyCompanyName.Erp";
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<ErpDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}{name}.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ErpDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}{name}.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ErpApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}{name}.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ErpApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}{name}.Application"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<WebSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MyCompanyName.Web.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ErpWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

        }
    }
}

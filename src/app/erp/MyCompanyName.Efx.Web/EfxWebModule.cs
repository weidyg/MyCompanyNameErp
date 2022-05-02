using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.Abp.Security;
using MyCompanyName.Erp;
using MyCompanyName.Erp.Localization;
using MyCompanyName.Web.Shared;
using System.IO;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SecurityLog;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.Efx.Web
{
    [DependsOn(
         typeof(WebSharedModule)
         )]
    public class EfxWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                   typeof(ErpResource),
                   typeof(WebSharedModule).Assembly,
                   typeof(EfxWebModule).Assembly
               );
            });
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            ConfigureVirtualFileSystem(services);
            ConfigureMenus();
            ConfigureSecurityLog();
        }
        private void ConfigureMenus()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyCompanyNameEfxMenuContributor());
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
                    options.FileSets.ReplaceEmbeddedByPhysical<EfxWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }
        private void ConfigureSecurityLog()
        {
            Configure<AbpSecurityLogOptions>(options =>
            {
                options.IsEnabled = true;
                options.ApplicationName = ClientType.EFX.ToString();
            });
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            //else { app.UseExceptionHandler("/Error"); app.UseStatusCodePagesWithRedirects("/Error/{0}"); }
            //app.UseStatusCodePagesWithReExecute("/Error", "?httpStatusCode={0}");
            //app.UseStatusCodePagesWithRedirects("/Error?httpStatusCode={0}");
        }
    }
}

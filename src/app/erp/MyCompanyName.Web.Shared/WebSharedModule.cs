using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Razui.Bundling;
using MyCompanyName.Abp.Security;
using MyCompanyName.Erp;
using MyCompanyName.Erp.DbMigrationsForMainDb.EntityFrameworkCore;
using MyCompanyName.Erp.EntityFrameworkCore;
using MyCompanyName.Erp.Localization;
using MyCompanyName.Erp.MultiTenancy;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Erp.Permissions.Identity;
using MyCompanyName.Identity;
using MyCompanyName.Identity.AspNetCore;
using MyCompanyName.Identity.Permissions;
using MyCompanyName.Web.Shared.Bundling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.Web.Shared
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpUiNavigationModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAutofacModule),
        typeof(IdentityAspNetCoreModule),
        typeof(ErpHttpApiModule),
        typeof(ErpApplicationModule),
        typeof(ErpEntityFrameworkCoreMainDbMigrationsModule),
        typeof(ErpEntityFrameworkCoreDbMigrationsModule),
        typeof(RazuiBundlingModule)
        )]
    public class WebSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(ErpResource),
                    typeof(ErpDomainModule).Assembly,
                    typeof(ErpDomainSharedModule).Assembly,
                    typeof(ErpApplicationModule).Assembly,
                    typeof(ErpApplicationContractsModule).Assembly,
                    typeof(WebSharedModule).Assembly
                );
            });
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            ConfigureLocalizationServices();
            ConfigureAutoApiControllers();
            ConfigureAuthentication(services);
            ConfigureVirtualFileSystem(services);
            ConfigureMultiTenancy(services);
            ConfigureCors(services);
            ConfigureSwaggerServices(services);
            //ConfigureAutoMapper(services);
            ConfigureException(services);
            ConfigureUrls(services);
            //ConfigureMenus();
            ConfigurePermissions();
            ConfigureBundling();
            ConfigureClient();
            //ConfigureSecurityLog();
        }
        private void ConfigureUrls(IServiceCollection services)
        {
            //var configuration = services.GetConfiguration();
            //Configure<AppUrlOptions>(options =>
            //{
            //    options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            //});
        }

        private void ConfigureClient()
        {
            Configure<ClientOptions>(options =>
            {
                options.ClientTypeMap.Add("Erp_Web", ClientType.ERP);
                options.ClientTypeMap.Add("Efx_Web", ClientType.EFX);
            });
        }

        private void ConfigureBundling()
        {
            Configure<BundlingOptions>(options =>
            {
                options.StyleBundles.Add(StandardBundles.Styles.Lib, bundle =>
                {
                    bundle.AddContributors(typeof(SharedLibStyleContributor));
                });
                options.ScriptBundles.Add(StandardBundles.Scripts.Lib, bundle =>
                {
                    bundle.AddContributors(typeof(SharedLibScriptContributor));
                });
            });
        }
        private void ConfigureException(IServiceCollection services)
        {
            //var hostingEnvironment = services.GetHostingEnvironment();
            //services.Configure<AbpExceptionHandlingOptions>(options =>
            //{
            //    options.SendExceptionsDetailsToClients = hostingEnvironment.IsDevelopment();
            //});
        }

        private void ConfigurePermissions()
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.ManagementProviders.Add<RolePermissionManagementProvider>();
                options.ManagementProviders.Add<UserPermissionManagementProvider>();
                options.ManagementProviders.Add<SystemAdminPermissionManagementProvider>();

                options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = IdentityPermissions.Users.ManagePermissions;
                options.ProviderPolicies[RolePermissionValueProvider.ProviderName] = IdentityPermissions.Roles.ManagePermissions;
            });
        }

        /// <summary>
        /// 配置多租户
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureMultiTenancy(IServiceCollection services)
        {
            services.Configure<AbpAspNetCoreMultiTenancyOptions>(options =>
            {
                options.TenantKey = "2JrO6kzZpDf8ka5cUH3r";
            });
        }

        /// <summary>
        /// 虚拟文件系统
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureVirtualFileSystem(IServiceCollection services)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<WebSharedModule>(typeof(WebSharedModule).Assembly.GetName().Name);
            });
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<ErpResource>();
                options.Resources.Add<WebSharedResource>("zh-Hans").AddVirtualJson("/Localization/Resource");
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("en", "en", "English(测试)"));
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ErpApplicationModule).Assembly);
                options.ConventionalControllers.Create(typeof(IdentityApplicationModule).Assembly);
            });
        }

        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            var basePath = Path.GetDirectoryName(typeof(ErpApplicationModule).Assembly.Location);
            var xmlPath = Path.Combine(basePath, "MyCompanyName.Erp.Application.xml");
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Erp API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomOperationIds(d => (d.ActionDescriptor as ControllerActionDescriptor)?.ActionName);
                    options.CustomSchemaIds(type => type.FullName);
                    options.IncludeXmlComments(xmlPath, true);
                }
            );
        }
        private const string DefaultCorsPolicyName = "Default";
        private void ConfigureCors(IServiceCollection services)
        {
            var configuration = services.GetConfiguration();
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            #region 
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
            //    {
            //        option.LoginPath = new PathString("/Login");
            //        option.AccessDeniedPath = new PathString("/Error/403");
            //    });

            //services
            //    .AddAuthentication()
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = configuration["AuthServer:Authority"];
            //        options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            //        options.Audience = "Erp";
            //    }); 
            #endregion
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Login");
                options.LogoutPath = new PathString("/Logout");
                options.AccessDeniedPath = new PathString("/Error/403");
            });
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
            //在这配置页面权限或在 PageModel 上配置 Authorize 属性
            Configure<RazorPagesOptions>(options =>
            {
                //options.Conventions.AuthorizePage("/");
                //options.Conventions.AuthorizePage("/Reseller/Level", ResellerPermissions.Level.Default);
                //options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            //if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            //else { app.UseExceptionHandler("/Error"); app.UseStatusCodePagesWithRedirects("/Error/{0}"); }
            //app.UseStatusCodePagesWithReExecute("/Error", "?httpStatusCode={0}");
            //app.UseStatusCodePagesWithRedirects("/Error?httpStatusCode={0}");
            app.UseAbpRequestLocalization(options =>
            {
                options.RequestCultureProviders = new List<IRequestCultureProvider> {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            //app.UseJwtTokenMiddleware();
            //app.UseIdentityServer();
            if (MultiTenancyConsts.IsEnabled) { app.UseMultiTenancy(); } //放 UseAuthentication 之后
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Erp API"); });
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

        }
    }
}

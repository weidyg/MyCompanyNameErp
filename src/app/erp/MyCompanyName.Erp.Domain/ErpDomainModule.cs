using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using MyCompanyName.Abp.Blob;
using MyCompanyName.Abp.Sms;
using MyCompanyName.Erp.MultiTenancy;
using MyCompanyName.Erp.ObjectExtending;
using MyCompanyName.Erp.PermissionManagement;
using MyCompanyName.Identity;
using MyCompanyName.TenantManagement;
using System;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(AbpMultiTenancyModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpLocalizationModule),
        typeof(AbpValidationModule),
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpDddDomainModule),
        typeof(TenantManagementDomainModule),
        typeof(IdentityDomainModule),
        typeof(MyCompanyNameAbpBlobModule),
        typeof(MyCompanyNameAbpSmsModule),
        typeof(ErpDomainSharedModule)
    )]
    public class ErpDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ErpDomainObjectExtensions.Configure();
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpPermissionOptions>(options =>
            {
                options.ValueProviders.Add<SystemAdminPermissionValueProvider>();
            });

            #region 配置多租户
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
                //模式1、共享数据库，2、每个租户单独数据库，3混合模式，默认是混合模式
                //options.DatabaseStyle = MultiTenancyDatabaseStyle.PerTenant;
            });
            #endregion

            #region 配置缓存
            Configure<AbpDistributedCacheOptions>(options =>
              {
                  //配置缓存
                  options.KeyPrefix = "MyCompanyName.Erp_";
                  options.GlobalCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(20));
                  options.CacheConfigurators.Add(cacheName => { return null; });
              });
            //配置Redis缓存或在 appsettings.json 配置
            Configure<RedisCacheOptions>(options => { });
            #endregion
        }
    }
}

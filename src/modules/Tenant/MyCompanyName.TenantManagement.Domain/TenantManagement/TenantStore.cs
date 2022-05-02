using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;

namespace MyCompanyName.TenantManagement
{
    public class TenantStore : ITenantStore, ITransientDependency
    {
        protected ITenantRepository TenantRepository { get; }
        protected IObjectMapper<TenantManagementDomainModule> ObjectMapper { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IDistributedCache<TenantCacheItem> Cache { get; }

        public TenantStore(
            ITenantRepository tenantRepository,
            IObjectMapper<TenantManagementDomainModule> objectMapper,
            ICurrentTenant currentTenant,
            IDistributedCache<TenantCacheItem> cache)
        {
            TenantRepository = tenantRepository;
            ObjectMapper = objectMapper;
            CurrentTenant = currentTenant;
            Cache = cache;
        }

        [Obsolete("Use FindAsync method.")]
        public virtual TenantConfiguration Find(string name) => FindAsync(name).Result;

        [Obsolete("Use FindAsync method.")]
        public virtual TenantConfiguration Find(Guid id) => FindAsync(id).Result;

        public virtual async Task<TenantConfiguration> FindAsync(string name) => (await GetCacheItemAsync(null, name)).Value;

        public virtual async Task<TenantConfiguration> FindAsync(Guid id) => (await GetCacheItemAsync(id, null)).Value;

        protected virtual async Task<TenantCacheItem> GetCacheItemAsync(Guid? id, string name)
        {
            var cacheKey = CalculateCacheKey(id, name);
            var cacheItem = await Cache.GetAsync(cacheKey, considerUow: true);
            if (cacheItem != null) { return cacheItem; }
            if (id.HasValue)
            {
                using (CurrentTenant.Change(null))
                {
                    var tenant = await TenantRepository.FindAsync(id.Value);
                    return await SetCacheAsync(cacheKey, tenant);
                }
            }
            if (!name.IsNullOrWhiteSpace())
            {
                using (CurrentTenant.Change(null))
                {
                    var tenant = await TenantRepository.FindByNameAsync(name);
                    return await SetCacheAsync(cacheKey, tenant);
                }
            }
            throw new AbpException("Both id and name can't be invalid.");
        }

        protected virtual async Task<TenantCacheItem> SetCacheAsync(string cacheKey, [CanBeNull] Tenant tenant)
        {
            var tenantConfiguration = tenant != null ? ObjectMapper.Map<Tenant, TenantConfiguration>(tenant) : null;
            var cacheItem = new TenantCacheItem(tenantConfiguration);
            await Cache.SetAsync(cacheKey, cacheItem, considerUow: true);
            return cacheItem;
        }

        protected virtual string CalculateCacheKey(Guid? id, string name)
        {
            return TenantCacheItem.CalculateCacheKey(id, name);
        }
    }
}

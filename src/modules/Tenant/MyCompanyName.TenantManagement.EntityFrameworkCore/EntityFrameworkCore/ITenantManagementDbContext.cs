using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.TenantManagement.EntityFrameworkCore
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(TenantManagementDbProperties.ConnectionStringName)]
    public interface ITenantManagementDbContext : IEfCoreDbContext
    {
        DbSet<Tenant> Tenants { get; }

        DbSet<TenantConnectionString> TenantConnectionStrings { get; }
    }
}
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.Identity.EntityFrameworkCore
{
    [ConnectionStringName(IdentityDbProperties.ConnectionStringName)]
    public interface IIdentityDbContext : IEfCoreDbContext
    {
        public DbSet<Company> Companys { get; set; }

        DbSet<IdentityUser> Users { get; }

        DbSet<IdentityRole> Roles { get; }

        DbSet<OrganizationUnit> OrganizationUnits { get; }

        DbSet<IdentitySecurityLog> SecurityLogs { get; }

        //DbSet<IdentityClaimType> ClaimTypes { get; }
        DbSet<IdentityLinkUser> LinkUsers { get; }
    }
}
using Microsoft.EntityFrameworkCore;
using MyCompanyName.Abp.EntityFrameworkCore;
using Volo.Abp.Data;

namespace MyCompanyName.Identity.EntityFrameworkCore
{
    [ConnectionStringName(IdentityDbProperties.ConnectionStringName)]
    public class IdentityDbContext : MyCompanyNameAbpDbContext<IdentityDbContext>, IIdentityDbContext
    {
        public DbSet<Company> Companys { get; set; }

        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

        //public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIdentity();
        }
    }
}
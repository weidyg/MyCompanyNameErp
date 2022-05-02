using Microsoft.EntityFrameworkCore;
using MyCompanyName.Erp.Entities;
using MyCompanyName.Erp.Entities.Finance;
using MyCompanyName.Erp.Entities.Reseller;
using MyCompanyName.Erp.Permissions;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    public static class ErpDbContextModelCreatingExtensions
    {
        public static void ConfigureErp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            #region Finance
            var FinanceDbTablePrefix = $"{ErpDbProperties.DbTablePrefix}Finance_";
            builder.Entity<BankCard>(b =>
            {
                b.ToTable($"{FinanceDbTablePrefix}BankCard", ErpDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.BankName).HasMaxLength(50);
                b.Property(x => x.CardType).HasMaxLength(50);
                b.Property(x => x.AccountNo).HasMaxLength(255);
                b.Property(x => x.RealName).HasMaxLength(50);
            });

            #endregion

            #region Reseller
            var ResellerDbTablePrefix = $"{ErpDbProperties.DbTablePrefix}Reseller_";
            builder.Entity<ResellerLevel>(b =>
            {
                b.ToTable($"{ResellerDbTablePrefix}Level", ErpDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasMaxLength(50);
                b.Property(x => x.Discount).HasPrecision(3, 2);
                b.Property(x => x.Description).HasMaxLength(500);
            });

            #endregion

            #region Setting
            var SystemDbTablePrefix = $"{ErpDbProperties.DbTablePrefix}System_";
            builder.Entity<PermissionGrant>(b =>
            {
                b.ToTable($"{SystemDbTablePrefix}PermissionGrants", ErpDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasMaxLength(128).IsRequired();
                b.Property(x => x.ProviderName).HasMaxLength(64).IsRequired();
                b.Property(x => x.ProviderKey).HasMaxLength(64).IsRequired();
                b.HasIndex(x => new { x.Name, x.ProviderName, x.ProviderKey });
            });
            #endregion
        }
    }
}
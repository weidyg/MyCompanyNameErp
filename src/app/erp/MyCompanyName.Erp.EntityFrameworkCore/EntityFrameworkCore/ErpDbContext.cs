using Microsoft.EntityFrameworkCore;
using MyCompanyName.Abp.EntityFrameworkCore;
using MyCompanyName.Erp.Entities;
using MyCompanyName.Erp.Entities.Finance;
using MyCompanyName.Erp.Entities.Reseller;
using MyCompanyName.Erp.Permissions;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.Erp.EntityFrameworkCore
{
    [ConnectionStringName(ErpDbProperties.ConnectionStringName)]
    public class ErpDbContext : MyCompanyNameAbpDbContext<ErpDbContext>
    {

        #region Finance
        public virtual DbSet<BankCard> BankCard { get; set; }
        //public virtual DbSet<TrpPayConfigDo> TrpPayConfig { get; set; }
        //public virtual DbSet<PayAccountInfoDo> PayAccountInfo { get; set; }
        //public virtual DbSet<PayAccountDetailDo> PayAccountDetail { get; set; }
        //public virtual DbSet<TradeOrderInfoDo> TradeOrderInfo { get; set; }
        //public virtual DbSet<TradeOrderItemDo> TradeOrderItem { get; set; }
        #endregion

        #region Finance
        public virtual DbSet<ResellerLevel> ResellLevel { get; set; }
        //public virtual DbSet<TrpPayConfigDo> TrpPayConfig { get; set; }
        //public virtual DbSet<PayAccountInfoDo> PayAccountInfo { get; set; }
        //public virtual DbSet<PayAccountDetailDo> PayAccountDetail { get; set; }
        //public virtual DbSet<TradeOrderInfoDo> TradeOrderInfo { get; set; }
        //public virtual DbSet<TradeOrderItemDo> TradeOrderItem { get; set; }
        #endregion

        #region System
        public DbSet<PermissionGrant> PermissionGrants { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }

        //public DbSet<IdentityRole> Roles { get; set; }

        //public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        #endregion

        /*在这里为聚合根/实体添加DbSet属性。
         * 也将它们映射到MyCompanyNameErpDbContextModelCreatingExtensions.ConfigureMyCompanyNameErp
         */

        public ErpDbContext(DbContextOptions<ErpDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*在此配置共享表（包含模块） */

            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

            //    b.ConfigureByConvention();
            //    b.ConfigureAbpUser();

            //    /* 为您的其他属性配置映射
            //     * 另请参阅MyCompanyNameErpEfCoreEntityExtensionMappings类
            //     */
            //});

            //builder.ConfigureTenantManagement();
            /* 在ConfigureMyCompanyNameErp方法中配置自己的表/实体 */
            builder.ConfigureErp();
            builder.UseMySQL();
        }
    }
}

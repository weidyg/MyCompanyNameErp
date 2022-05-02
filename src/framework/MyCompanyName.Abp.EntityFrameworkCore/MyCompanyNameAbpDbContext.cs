using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using MyCompanyName.Abp.Company;
using MyCompanyName.Abp.DataFilter;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Security.Principal;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Reflection;
using Volo.Abp.Security.Claims;

namespace MyCompanyName.Abp.EntityFrameworkCore
{
    public class MyCompanyNameAbpDbContext<TDbContext> : AbpDbContext<TDbContext> where TDbContext : DbContext
    {
        protected MyCompanyNameAbpDbContext(DbContextOptions<TDbContext> options)
          : base(options)
        {

        }

        #region DataFilter
        protected bool MultiClientFilterEnabled => DataFilter?.IsEnabled<IMultiClient>() ?? false;
        public ICurrentPrincipalAccessor CurrentPrincipalAccessor => LazyServiceProvider.LazyGetRequiredService<ICurrentPrincipalAccessor>();
        protected virtual string CurrentClientType => CurrentPrincipalAccessor?.Principal?.FindClientType();


        protected bool MultiCompanyFilterEnabled => DataFilter?.IsEnabled<IMultiCompany>() ?? false;
        public ICurrentCompany CurrentCompany => LazyServiceProvider.LazyGetRequiredService<ICurrentCompany>();
        protected virtual Guid? CurrentCompanyId => CurrentCompany?.Id;


        protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
        {
            if (typeof(IMultiClient).IsAssignableFrom(typeof(TEntity))) { return true; }
            if (typeof(IMultiCompany).IsAssignableFrom(typeof(TEntity))) { return true; }
            return base.ShouldFilterEntity<TEntity>(entityType);
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            var expression = base.CreateFilterExpression<TEntity>();
            if (typeof(IMultiClient).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> multiClientFilter = e => !MultiClientFilterEnabled || EF.Property<string>(e, "ClientType") == CurrentClientType;
                expression = expression == null ? multiClientFilter : CombineExpressions(expression, multiClientFilter);
            }
            if (typeof(IMultiCompany).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> multiCompanyFilter = e => !MultiCompanyFilterEnabled || EF.Property<Guid>(e, "CompanyId") == CurrentCompanyId;
                expression = expression == null ? multiCompanyFilter : CombineExpressions(expression, multiCompanyFilter);
            }
            return expression;
        }
        #endregion

        protected override void ApplyAbpConceptsForAddedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            CheckClientType(entry);
            CheckTenantId(entry);
            CheckCompanyId(entry);
            base.ApplyAbpConceptsForAddedEntity(entry, changeReport);
        }
        #region TryToSetTenantId
        protected virtual void CheckTenantId(EntityEntry entry)
        {
            if (entry.Entity is IMultiTenant entityWithGuidId)
            {
                TrySetTenantId(entry, entityWithGuidId);
            }
        }
        protected virtual void TrySetTenantId(EntityEntry entry, IMultiTenant entity)
        {
            if (!IsMultiTenantFilterEnabled || entity.TenantId != default) { return; }
            var idProperty = entry.Property(nameof(IMultiTenant.TenantId)).Metadata.PropertyInfo;
            var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
            if (dbGeneratedAttr != null && dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.None) { return; }
            ObjectHelper.TrySetProperty(entity, x => x.TenantId, () => CurrentTenantId, Array.Empty<Type>());
        }
        #endregion

        #region TryToSetClientType
        protected virtual void CheckClientType(EntityEntry entry)
        {
            if (entry.Entity is IMultiClient entityWithType)
            {
                TrySetClientType(entry, entityWithType);
            }
        }
        protected virtual void TrySetClientType(EntityEntry entry, IMultiClient entity)
        {
            if (!MultiClientFilterEnabled || entity.ClientType != default) { return; }
            var idProperty = entry.Property(nameof(IMultiClient.ClientType)).Metadata.PropertyInfo;
            var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
            if (dbGeneratedAttr != null && dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.None) { return; }
            ObjectHelper.TrySetProperty(entity, x => x.ClientType, () => CurrentClientType, Array.Empty<Type>());
        }
        #endregion

        #region TryToSetCompanyId
        protected virtual void CheckCompanyId(EntityEntry entry)
        {
            if (entry.Entity is IMultiCompany entityWithGuidId)
            {
                TrySetCompanyId(entry, entityWithGuidId);
            }
        }
        protected virtual void TrySetCompanyId(EntityEntry entry, IMultiCompany entity)
        {
            if (!MultiCompanyFilterEnabled || entity.CompanyId != default) { return; }
            var idProperty = entry.Property(nameof(IMultiCompany.CompanyId)).Metadata.PropertyInfo;
            var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
            if (dbGeneratedAttr != null && dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.None) { return; }
            ObjectHelper.TrySetProperty(entity, x => x.CompanyId, () => CurrentCompanyId, Array.Empty<Type>());
        }
        #endregion
    }
}

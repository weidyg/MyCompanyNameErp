using JetBrains.Annotations;
using MyCompanyName.Abp.DataFilter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public class IdentityRole : AggregateRoot<Guid>, IMultiTenant, IMultiCompany
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual Guid? CompanyId { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; protected internal set; }

        /// <summary>
        /// 规范化名称
        /// </summary>
        [DisableAuditing]
        public virtual string NormalizedName { get; protected internal set; }

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim> Claims { get; protected set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// 是否静态(不能重命名，不能删除)
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// 用户可以看到其他用户的公共角色
        /// </summary>
        public virtual bool IsPublic { get; set; }

        ///// <summary>
        ///// 是否启用
        ///// </summary>
        //public bool IsActive { get; set; }

        protected IdentityRole() { }

        public IdentityRole(Guid id, [NotNull] string name, Guid? tenantId = null)
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
            TenantId = tenantId;
            NormalizedName = name.ToUpperInvariant();
            ConcurrencyStamp = Guid.NewGuid().ToString();

            Claims = new Collection<IdentityRoleClaim>();
        }

        public virtual void AddClaim([NotNull] IGuidGenerator guidGenerator, [NotNull] Claim claim)
        {
            Check.NotNull(guidGenerator, nameof(guidGenerator));
            Check.NotNull(claim, nameof(claim));

            Claims.Add(new IdentityRoleClaim(guidGenerator.Create(), Id, claim, TenantId));
        }

        public virtual void AddClaims([NotNull] IGuidGenerator guidGenerator, [NotNull] IEnumerable<Claim> claims)
        {
            Check.NotNull(guidGenerator, nameof(guidGenerator));
            Check.NotNull(claims, nameof(claims));

            foreach (var claim in claims)
            {
                AddClaim(guidGenerator, claim);
            }
        }

        public virtual IdentityRoleClaim FindClaim([NotNull] Claim claim)
        {
            Check.NotNull(claim, nameof(claim));

            return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
        }

        public virtual void RemoveClaim([NotNull] Claim claim)
        {
            Check.NotNull(claim, nameof(claim));

            Claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
        }

        public virtual void ChangeName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var oldName = Name;
            Name = name;
            AddDistributedEvent(new IdentityRoleNameChangedEto
            {
                Id = Id,
                Name = Name,
                OldName = oldName,
                TenantId = TenantId
            });
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Name = {Name}";
        }
    }
}
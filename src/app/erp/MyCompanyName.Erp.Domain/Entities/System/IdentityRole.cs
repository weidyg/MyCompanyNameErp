using Sunton.Erp.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Sunton.Erp.Entities.System
{
    public class IdentityRole : AggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public virtual string Name { get; protected internal set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        [DisableAuditing]
        public virtual string NormalizedName { get; protected internal set; }

        /// <summary>
        /// A default role is automatically assigned to a new user
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// A static role can not be deleted/renamed
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// A user can see other user's public roles
        /// </summary>
        public virtual bool IsPublic { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole"/>.
        /// </summary>
        protected IdentityRole() { }

        public IdentityRole(Guid id, [NotNull] string name, Guid? tenantId = null)
        {
            Check.NotNull(name, nameof(name));
            Id = id;
            Name = name;
            TenantId = tenantId;
            NormalizedName = name.ToUpperInvariant();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        public virtual void ChangeName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var oldName = Name;
            Name = name;
            AddLocalEvent(
                new IdentityRoleNameChangedEvent
                {
                    IdentityRole = this,
                    OldName = oldName
                }
            );
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Name = {Name}";
        }
    }
}
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Sunton.Erp.Entities.System
{
    public class IdentityUser : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual string UserName { get; protected internal set; }
        public virtual string Name { get; set; }
        [DisableAuditing]
        public virtual string PasswordHash { get; protected internal set; }

        public virtual string Email { get; protected internal set; }

        [CanBeNull]
        public virtual string PhoneNumber { get; protected internal set; }


        public virtual bool EmailConfirmed { get; protected internal set; }
        public virtual bool PhoneNumberConfirmed { get; protected internal set; }

        [DisableAuditing]
        public virtual string NormalizedUserName { get; protected internal set; }
        [DisableAuditing]
        public virtual string NormalizedEmail { get; protected internal set; }

        [DisableAuditing]
        public virtual string SecurityStamp { get; protected internal set; }

        public virtual ICollection<IdentityRole> Roles { get; protected set; }

        public virtual ICollection<IdentityUserOrganizationUnit> OrganizationUnits { get; protected set; }


        protected IdentityUser()
        {
        }

        public IdentityUser(
          Guid id,
          [NotNull] string userName,
          [NotNull] string email,
          Guid? tenantId = null)
          : base(id)
        {
            Check.NotNull(userName, nameof(userName));
            Check.NotNull(email, nameof(email));

            TenantId = tenantId;
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            ConcurrencyStamp = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();

            Roles = new Collection<IdentityRole>();
            OrganizationUnits = new Collection<IdentityUserOrganizationUnit>();
        }

        public virtual bool IsInOrganizationUnit(Guid organizationUnitId)
        {
            return OrganizationUnits.Any(
                ou => ou.OrganizationUnitId == organizationUnitId
            );
        }
    }
}

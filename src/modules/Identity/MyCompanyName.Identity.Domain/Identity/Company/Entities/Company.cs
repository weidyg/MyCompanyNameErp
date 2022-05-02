using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace MyCompanyName.Identity
{
    public class Company : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }

        public virtual ICollection<CompanyLinkTenant> LinkTenants { get; protected set; }

        protected Company()
        {

        }

        public Company(Guid id, [NotNull] string name)
            : base(id)
        {
            SetName(name);
            LinkTenants = new Collection<CompanyLinkTenant>();
        }

        //public virtual bool IsInRole(Guid roleId)
        //{
        //    Check.NotNull(roleId, nameof(roleId));
        //    return LinkTenants.Any(r => r.Id == roleId);
        //}

        public virtual void AddLinkTenant([NotNull] IGuidGenerator guidGenerator,
            string clientType,
            Guid tenantId)
        {
            Check.NotNull(guidGenerator, nameof(guidGenerator));
            Check.NotNull(clientType, nameof(clientType));
            LinkTenants.Add(new CompanyLinkTenant(guidGenerator.Create(), Id, clientType, tenantId));
        }

        public virtual IEnumerable<CompanyLinkTenant> FindLinkTenants([NotNull] string clientType, Guid? tenantId = null)
        {
            Check.NotNull(clientType, nameof(clientType));
            var tempLinkTenants = LinkTenants.Where(c => c.ClientType == clientType && c.CompanyId == Id);
            if (tenantId != null) { tempLinkTenants.Where(w => w.TenantId == tenantId); }
            return tempLinkTenants;
        }

        protected internal virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), IdentityCompanyConsts.MaxNameLength);
        }
    }
}

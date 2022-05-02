using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyCompanyName.Identity
{
    public class CompanyLinkTenant : CreationAuditedEntity<Guid>
    {
        public virtual Guid CompanyId { get; protected set; }

        public virtual Guid TenantId { get; protected set; }

        public virtual string ClientType { get; protected set; }

        //protected CompanyLinkTenant()
        //{

        //}

        public CompanyLinkTenant(Guid id, 
            Guid companyId, 
            string clientType, 
            Guid tenantId):base(id)
        {
            CompanyId = companyId;
            ClientType = clientType;
            TenantId = tenantId;
        }

        public override object[] GetKeys()
        {
            return new object[] { CompanyId, TenantId, ClientType };
        }
    }
}
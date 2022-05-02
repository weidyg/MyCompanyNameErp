using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public class IdentityUserOrganizationUnit : CreationAuditedEntity
        //, IMultiTenant
        , IMultiCompany
    {
        ///// <summary>
        ///// TenantId of this entity.
        ///// </summary>
        //public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? CompanyId { get; protected set; }

        /// <summary>
        /// Id of the User.
        /// </summary>
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// Id of the related <see cref="OrganizationUnit"/>.
        /// </summary>
        public virtual Guid OrganizationUnitId { get; protected set; }

        protected IdentityUserOrganizationUnit()
        {

        }

        public IdentityUserOrganizationUnit(Guid userId, Guid organizationUnitId, Guid? companyId = null)
        {
            UserId = userId;
            OrganizationUnitId = organizationUnitId;
            CompanyId = companyId;
        }

        public override object[] GetKeys()
        {
            return new object[] { UserId, OrganizationUnitId };
        }
    }
}
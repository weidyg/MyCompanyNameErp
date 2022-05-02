using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public class IdentityUserRole : Entity
        , IMultiCompany
    //, IMultiTenant
    {
        public virtual Guid? CompanyId { get; protected set; }
        //public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public virtual Guid RoleId { get; protected set; }

        protected IdentityUserRole()
        {

        }

        protected internal IdentityUserRole(Guid userId, Guid roleId, Guid? companyId)
        {
            UserId = userId;
            RoleId = roleId;
            CompanyId = companyId;
        }

        public override object[] GetKeys()
        {
            return new object[] { UserId, RoleId };
        }
    }
}
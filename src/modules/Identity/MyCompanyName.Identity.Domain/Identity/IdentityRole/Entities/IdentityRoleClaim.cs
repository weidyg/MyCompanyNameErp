using JetBrains.Annotations;
using System;
using System.Security.Claims;

namespace MyCompanyName.Identity
{
    public class IdentityRoleClaim : IdentityClaim
    {
        public virtual Guid RoleId { get; protected set; }

        protected IdentityRoleClaim()
        {

        }

        protected internal IdentityRoleClaim(Guid id, Guid roleId, [NotNull] Claim claim, Guid? tenantId)
            : base(id, claim, tenantId)
        {
            RoleId = roleId;
        }

        public IdentityRoleClaim(Guid id, Guid roleId, [NotNull] string claimType, string claimValue, Guid? tenantId)
            : base(id, claimType, claimValue, tenantId)
        {
            RoleId = roleId;
        }
    }
}
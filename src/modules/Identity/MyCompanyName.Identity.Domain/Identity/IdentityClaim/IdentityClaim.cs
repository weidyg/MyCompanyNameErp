using JetBrains.Annotations;
using MyCompanyName.Abp.DataFilter;
using System;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public abstract class IdentityClaim : Entity<Guid>
        , IMultiCompany
        //, IMultiTenant
    {
        //public virtual Guid? TenantId { get; protected set; }
        public virtual Guid? CompanyId { get; protected set; }

        public virtual string ClaimType { get; protected set; }

        public virtual string ClaimValue { get; protected set; }

        protected IdentityClaim()
        {

        }

        protected internal IdentityClaim(Guid id, [NotNull] Claim claim, Guid? companyId)
            : this(id, claim.Type, claim.Value, companyId)
        {

        }

        protected internal IdentityClaim(Guid id, [NotNull] string claimType, string claimValue, Guid? companyId)
        {
            Check.NotNull(claimType, nameof(claimType));
            Id = id;
            ClaimType = claimType;
            ClaimValue = claimValue;
            CompanyId = companyId;
        }

        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        public virtual void SetClaim([NotNull] Claim claim)
        {
            Check.NotNull(claim, nameof(claim));
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
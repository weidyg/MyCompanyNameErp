using JetBrains.Annotations;
using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public class IdentityUserToken : Entity, IMultiCompany
    //, IMultiTenant
    {
        //public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? CompanyId { get; protected set; }

        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public virtual string LoginProvider { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public virtual string Value { get; set; }

        protected IdentityUserToken()
        {

        }

        protected internal IdentityUserToken(
            Guid userId,
            [NotNull] string loginProvider,
            [NotNull] string name,
            string value,
            Guid? companyId)
        {
            Check.NotNull(loginProvider, nameof(loginProvider));
            Check.NotNull(name, nameof(name));

            UserId = userId;
            LoginProvider = loginProvider;
            Name = name;
            Value = value;
            CompanyId = companyId;
        }

        public override object[] GetKeys()
        {
            return new object[] { UserId, LoginProvider, Name };
        }
    }
}
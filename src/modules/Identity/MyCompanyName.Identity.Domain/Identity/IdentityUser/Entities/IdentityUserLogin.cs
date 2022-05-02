using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Identity
{
    public class IdentityUserLogin : Entity, IMultiCompany
    //, IMultiTenant
    {
        //public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? CompanyId { get; protected set; }

        /// <summary>
        /// Gets or sets the of the primary key of the user associated with this login.
        /// </summary>
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; protected set; }

        /// <summary>
        /// Gets or sets the unique provider identifier for this login.
        /// </summary>
        public virtual string ProviderKey { get; protected set; }

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login.
        /// </summary>
        public virtual string ProviderDisplayName { get; protected set; }

        protected IdentityUserLogin()
        {

        }

        protected internal IdentityUserLogin(
            Guid userId,
            [NotNull] string loginProvider,
            [NotNull] string providerKey,
            string providerDisplayName,
            Guid? companyId
            )
        {
            Check.NotNull(loginProvider, nameof(loginProvider));
            Check.NotNull(providerKey, nameof(providerKey));

            UserId = userId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            ProviderDisplayName = providerDisplayName;
            CompanyId = companyId;
        }

        protected internal IdentityUserLogin(
            Guid userId,
            [NotNull] UserLoginInfo login
            ,Guid? companyId
            )
            : this(
                  userId,
                  login.LoginProvider,
                  login.ProviderKey,
                  login.ProviderDisplayName
                  ,companyId
                  )
        {
        }

        public virtual UserLoginInfo ToUserLoginInfo()
        {
            return new UserLoginInfo(LoginProvider, ProviderKey, ProviderDisplayName);
        }

        public override object[] GetKeys()
        {
            return new object[] { UserId, LoginProvider };
        }
    }
}
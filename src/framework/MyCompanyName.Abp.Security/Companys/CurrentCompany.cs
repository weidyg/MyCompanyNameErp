using System;
using System.Security.Principal;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace MyCompanyName.Abp.Company
{
    public class CurrentCompany : ICurrentCompany, ITransientDependency
    {
        public virtual Guid? Id => _principalAccessor.Principal?.FindCompanyId();

        public virtual string Name => _principalAccessor.Principal?.FindCompanyName();

        public virtual Guid? TenantId => _principalAccessor.Principal?.FindTenantId();


        public virtual bool IsAuthenticated => Id != null;


        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentCompany(ICurrentPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }
    }
}

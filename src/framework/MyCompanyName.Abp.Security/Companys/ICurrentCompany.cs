using System;

namespace MyCompanyName.Abp.Company
{
    public interface ICurrentCompany
    {

        bool IsAuthenticated { get; }

        Guid? TenantId { get; }

        Guid? Id { get; }

        string Name { get; }
    }
}
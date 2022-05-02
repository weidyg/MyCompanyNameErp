using System;

namespace MyCompanyName.Abp.DataFilter
{
    public interface IMultiCompany
    {
        /// <summary>
        /// Id of the related company.
        /// </summary>
        Guid? CompanyId { get; }
    }
}

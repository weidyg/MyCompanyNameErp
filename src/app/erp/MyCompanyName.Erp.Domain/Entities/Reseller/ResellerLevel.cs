using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Erp.Entities.Reseller
{
    public class ResellerLevel : AuditedAggregateRoot<Guid>, IMultiTenant, IMultiCompany
    {
        /// <summary>
        /// 等级名称
        /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// 等级折扣(0-1)
        /// </summary>  
        public decimal Discount { get; set; }

        /// <summary>
        /// 等级描述
        /// </summary>   
        public string Description { get; set; }

        /// <summary>
        /// 是否设为默认等级
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        public Guid? TenantId { get; protected set; }

        public Guid? CompanyId { get; set; }
    }
}

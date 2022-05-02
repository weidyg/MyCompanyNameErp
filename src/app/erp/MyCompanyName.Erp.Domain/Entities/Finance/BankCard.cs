using MyCompanyName.Abp.DataFilter;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Erp.Entities.Finance
{
    public class BankCard : AuditedAggregateRoot<Guid>, IMultiTenant, IMultiCompany
    {
        /// <summary>
        /// 账号/卡号
        /// </summary>   
        public string AccountNo { get; set; }

        /// <summary>
        /// 银行/支付机构名称
        /// </summary>  
        public string BankName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>  
        public string CardType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>   
        public string RealName { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        public Guid? TenantId { get; protected set; }

        public Guid? CompanyId { get; set; }
    }
}

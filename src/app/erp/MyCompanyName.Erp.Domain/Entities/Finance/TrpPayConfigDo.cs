using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace SuntonErp.Entities.Finance
{
    [Table("Finance_TrpPayConfig")]
    public class TrpPayConfigDo : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 
        /// </summary>   
        [StringLength(50)]
        public string TrpPayName { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        [StringLength(50)]
        public string TrpPayCode { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string TrpPayLogo { get; set; }

        /// <summary>
        /// 签约功能
        /// </summary>   
        [StringLength(255)]
        public string PayProdCodes { get; set; }

        /// <summary>
        /// 支付平台手续费率
        /// </summary>   
        [Range(typeof(decimal), "0.000", "1.000")]
        public decimal ServiceFeeRate { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>   
        [StringLength(255)]
        public string AccountNo { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamOne { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamTwo { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamThree { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamFour { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamFive { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public string PayParamSix { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}

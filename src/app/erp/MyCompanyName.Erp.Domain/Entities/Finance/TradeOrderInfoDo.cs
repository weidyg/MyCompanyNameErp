using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuntonErp.Entities.Finance
{
    [Table("Finance_TradeOrderInfo")]
    public class TradeOrderInfoDo : BaseFullAudited<Guid>, IMustHaveCompany
    {
        #region 交易号
        /// <summary>
        /// 系统交易号
        /// </summary>
        [StringLength(50)]
        public string SysTradeNo { get; set; }

        /// <summary>
        /// 外部交易号
        /// </summary>
        [StringLength(50)]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付交易号(如:支付宝交易号，线下转账交易号等)
        /// </summary>   
        [StringLength(50)]
        public string PayTradeNo { get; set; }
        #endregion

        #region 描述类信息
        /// <summary>
        /// 业务类型
        /// </summary>
        [StringLength(50)]
        public string BusinessType { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [StringLength(255)]
        public string Subject { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string Body { get; set; }

        ///// <summary>
        ///// 支付端
        ///// </summary>
        //public string ViewType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string TradeMemo { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [StringLength(500)]
        public string AuditMemo { get; set; }
        #endregion

        #region 收付款信息
        /// <summary>
        /// 系统角色
        /// </summary>
        [StringLength(20)]
        public string SysRole { get; set; }

        /// <summary>
        /// 付款者公司Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 付款者公司名
        /// </summary>
        [StringLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 支付渠道
        /// </summary>
        [StringLength(50)]
        public string Channel { get; set; }

        /// <summary>
        /// 付款信息
        /// </summary>   
        //[StringLength(255)]
        public string PayerInfo { get; set; }

        /// <summary>
        /// 是否第三方支付
        /// </summary>
        public bool IsTrpPay { get; set; }

        /// <summary>
        /// 第三方支付用户(付款方)标识
        /// </summary>
        [StringLength(50)]
        public string TrpPayOpenId { get; set; }

        /// <summary>
        /// 收款信息
        /// </summary>   
        //[StringLength(255)]
        public string PayeeInfo { get; set; }

        /// <summary>
        /// 第三方支付配置信息
        /// </summary>
        public string TrpPayConfig { get; set; }

        /// <summary>
        /// 公用回传参数，如果请求时传递了该参数，则返回给商户时会回传该参数。
        /// 支付宝只会在同步返回（包括跳转回商户网站）和异步通知时将该参数原样返回。本参数必须进行UrlEncode之后才可以发送给支付宝。
        /// </summary>
        public string PassbackParams { get; set; }

        /// <summary>
        /// 第三方支付返回结果
        /// </summary>
        public string TrpPayResult { get; set; }
        #endregion

        #region 金额信息

        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }

        /// <summary>
        /// 总支付金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 实际支付金额（=TotalAmount+ServiceFee）
        /// </summary>
        public decimal PaidAmount { get; set; }
        #endregion

        #region 时间
        /// <summary>
        /// 付款时间
        /// </summary>   
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 付款结果确认时间/审核时间
        /// </summary>   
        public DateTime ConfirmTime { get; set; }
        #endregion

        #region 状态
        /// <summary>
        /// 交易状态
        /// </summary>
        [StringLength(50)]
        public string TradeStatus { get; set; }
        #endregion

        #region 通知Url
        /// <summary>
        /// 异步通知地址
        /// </summary>   
        [StringLength(500)]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 同步返回地址
        /// </summary>   
        [StringLength(500)]
        public string ReturnUrl { get; set; }
        #endregion

        #region 虚拟属性
        [ForeignKey("TradeOrderId")]
        public virtual ICollection<TradeOrderItemDo> TradeOrderItems { get; set; }
        #endregion

        /// <summary>
        /// 并发处理 版本控制 乐关锁
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }
    }
}

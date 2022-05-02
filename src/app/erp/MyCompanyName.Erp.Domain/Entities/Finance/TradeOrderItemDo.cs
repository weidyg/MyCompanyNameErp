using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuntonErp.Entities.Finance
{
    [Table("Finance_TradeOrderItem")]
    public class TradeOrderItemDo : BaseFullAudited
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(50)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }

        #region 虚拟属性
        /// <summary>
        /// 
        /// </summary>
        public Guid TradeOrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual TradeOrderInfoDo TradeOrder { get; set; }
        #endregion
    }
}

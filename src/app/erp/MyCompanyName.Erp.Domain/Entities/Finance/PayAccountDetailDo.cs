using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuntonErp.Entities.Finance
{
    [Table("Finance_AccountDetail")]
    public class PayAccountDetailDo : BaseFullAudited, IMustHaveCompany
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        [StringLength(50)]
        public string AccountType { get; set; }

        /// <summary>
        /// 交易主题
        /// </summary>
        [StringLength(50)]
        public string Subject { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [StringLength(50)]
        public string BusinessType { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>   
        public decimal Balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255)]
        public string SysTradeNo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNos { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Meno { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        [StringLength(50)]
        public string SysRole { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 付款者公司名
        /// </summary>
        [StringLength(50)]
        public string CompanyName { get; set; }

        #region 
        /// <summary>
        /// 账号
        /// </summary>   
        public int AccountId { get; set; }

        /// <summary>
        /// 账号信息
        /// </summary>
        public virtual PayAccountInfoDo Account { get; set; }
        #endregion
    }
}

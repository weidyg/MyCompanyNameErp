using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuntonErp.Entities.Finance
{
    [Table("Finance_AccountInfo")]
    public class PayAccountInfoDo : BaseFullAudited, IPassivable, IMustHaveCompany
    {
        /// <summary>
        /// 账号
        /// </summary>   
        [StringLength(50)]
        public string AccountNo { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>   
        public decimal Balance { get; set; }

        /// <summary>
        /// 是否开启授信
        /// </summary>   
        public bool IsCredit { get; set; }

        /// <summary>
        /// 授信总额
        /// </summary>   
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// 授信已用金额
        /// </summary>   
        public decimal CreditUsed { get; set; }

        /// <summary>
        /// 是否启用密码
        /// </summary>   
        public bool IsEnablePwd { get; set; }

        /// <summary>
        /// 付款密码
        /// </summary>   
        [StringLength(255)]
        public string PayPassword { get; set; }

        /// <summary>
        /// 安全手机号（用于找回密码等）
        /// </summary>   
        public string SecurityMobile { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// EFX、ERP
        /// </summary>
        public string SysRole { get; set; }

        /// <summary>
        /// 并发处理 版本控制 乐关锁
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }
    }
}

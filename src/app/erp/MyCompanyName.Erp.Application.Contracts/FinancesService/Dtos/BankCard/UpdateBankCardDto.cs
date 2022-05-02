namespace MyCompanyName.Erp.FinancesService
{
    public class UpdateBankCardDto
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
        /// 姓名
        /// </summary>   
        public string RealName { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { get; set; }

        public string CardType { get; set; }
    }
}
namespace MyCompanyName.Erp.SystemService
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSmsCaptchaDto
    {
        /// <summary>
        /// 短信编号
        /// </summary>
        public string SmsNo { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 时间戳(误差十分钟)
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
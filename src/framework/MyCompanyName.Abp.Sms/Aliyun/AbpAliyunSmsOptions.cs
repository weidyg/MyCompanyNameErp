namespace Volo.Abp.Sms.Aliyun
{
    public class AbpAliyunSmsOptions
    {
        public string EndPoint { get; set; } = "dysmsapi.aliyuncs.com";

        public string AccessKeyId { get; set; }

        public string AccessKeySecret { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using AlibabaCloud.TeaUtil;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tea;
using Tea.Utils;
using Volo.Abp.DependencyInjection;
using AliyunClient = AlibabaCloud.SDK.Dysmsapi20170525.Client;
using AliyunConfig = AlibabaCloud.OpenApiClient.Models.Config;
using AliyunSendSmsRequest = AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest;

namespace Volo.Abp.Sms.Aliyun
{
    public class AliyunSmsSender : ISmsSender, ITransientDependency
    {
        private readonly ILogger _logger;
        protected AbpAliyunSmsOptions Options { get; }

        public AliyunSmsSender(
            ILogger<AliyunSmsSender> logger,
            IOptionsSnapshot<AbpAliyunSmsOptions> options)
        {
            _logger = logger;
            Options = options.Value;
        }
        //https://next.api.aliyun.com/api/Dysmsapi/2017-05-25/SendSms?spm=a2c4g.11186623.2.12.6eef46ebUmA1fx&params={}&sdkStyle=dara&lang=CSHARP
        public async Task SendAsync(SmsMessage smsMessage)
        {
            if (Common.EqualString(Options?.AccessKeyId?.ToUpper(), "TEST")) { return; }
            var client = CreateClient();
            var sendSmsRequest = new AliyunSendSmsRequest
            {
                PhoneNumbers = smsMessage.PhoneNumber,
                SignName = smsMessage.Properties.GetOrDefault("SignName") as string,
                TemplateCode = smsMessage.Properties.GetOrDefault("TemplateCode") as string,
                TemplateParam = smsMessage.Text
            };
            var sendResp = new SendSmsResponse { Body = new SendSmsResponseBody() };
            try { sendResp = await client.SendSmsAsync(sendSmsRequest); }
            catch (TeaException ex)
            {
                sendResp.Body.Code = ex.Code;
                sendResp.Body.Message = ex.Message;
                sendResp.Body.RequestId = ex.DataResult.Get("RequestId") as string;
                _logger.LogError(ex, $"AliyunSmsSender_SendAsync_Data:{JsonConvert.SerializeObject(ex.DataResult)}");
            }

            string code = sendResp?.Body?.Code;
            if (!Common.EqualString(code, "OK"))
            {
                throw new UserFriendlyException("SentFailed", code, sendResp.Body.Message);
            }
        }

        protected virtual AliyunClient CreateClient()
        {
            return new(new AliyunConfig
            {
                AccessKeyId = Options.AccessKeyId,
                AccessKeySecret = Options.AccessKeySecret,
                Endpoint = Options.EndPoint,
            });
        }
    }
}
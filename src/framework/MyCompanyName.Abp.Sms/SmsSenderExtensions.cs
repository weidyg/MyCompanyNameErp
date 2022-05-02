using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Volo.Abp.Sms
{
    public static class SmsSenderExtensions
    {
        public static Task SendAsync([NotNull] this ISmsSender smsSender,
            [NotNull] string phoneNumber,
            [NotNull] string text,
            string signName,
            string templateCode
            )
        {
            Check.NotNull(smsSender, nameof(smsSender));
            var smsMessage = new SmsMessage(phoneNumber, text);
            smsMessage.Properties.Add("SignName", signName);
            smsMessage.Properties.Add("TemplateCode", templateCode);
            return smsSender.SendAsync(smsMessage);
        }
    }
}

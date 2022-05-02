using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Sms;

namespace MyCompanyName.Erp.SystemService
{
    public class ToolsAppService : ErpAppService, IToolsAppService
    {
        private readonly ISmsSender _smsSender;
        private readonly IDistributedCache _cache;
        public ToolsAppService(
            IDistributedCache cache,
            ISmsSender smsSender
            )
        {
            _cache = cache;
            _smsSender = smsSender;

        }

        [AllowAnonymous]
        public async Task SendSmsCaptchaAsync(SendSmsCaptchaDto param)
        {
            var requestTime = ((double)param.Timestamp).FromTimestamp();
            if (requestTime < Clock.Now.AddMinutes(-10) || requestTime > Clock.Now.AddMinutes(10))
            { throw new UserFriendlyException("非法请求，请求时间超出误差！"); }
            var sign = $"{param.Mobile}{param.Timestamp}{"rKNoSQdaHdrI2gVW"}".ToMd5();
            if (param.Sign.ToUpper() != sign.ToUpper()) { throw new UserFriendlyException("非法请求，签名错误！"); }
            var _cacheKey = $"Sms:{param.SmsNo}_{param.Mobile}";
            var captcha = await _cache.GetStringAsync(_cacheKey);
            if (captcha != null) { throw new UserFriendlyException("操作过于频繁，请稍后再试！"); }
            var code = RandomHelper.GetRandom(100000, 999999).ToString();
#if RELEASE
            await _smsSender.SendAsync(param.Mobile, $"{{code:{ code}}}", "福建商通", "SMS_168310215");
#endif
            await _cache.SetStringAsync(_cacheKey, code, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(120) });
        }
    }
}

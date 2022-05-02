using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp.SystemService
{
    public interface IToolsAppService : IApplicationService
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task SendSmsCaptchaAsync(SendSmsCaptchaDto param);
    }
}

using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp.ResellerService
{
    public interface IResellerInfoAppService : IApplicationService
    {
        Task<PagedResultDto<ResellerInfoDto>> GetPageListAsync(int pageNo, int pageSize, QueryResellerInfoDto param);

    }
}

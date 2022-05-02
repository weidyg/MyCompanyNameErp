using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp.SystemService
{
    public interface IPermissionAppService : IApplicationService
    {
        Task<GetPermissionListResultDto> GetAsync([NotNull] string providerName, [NotNull] string providerKey);

        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, UpdatePermissionsDto input);

    }
}

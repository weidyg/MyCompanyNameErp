using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Identity
{
    public interface IRoleAppService : IApplicationService
    {
        Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input);
        Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input);
        Task DeleteAsync(Guid id);

        Task<IdentityRoleDto> GetAsync(Guid id);

        Task<List<IdentityRoleDto>> GetListAsync(QueryIdentityRoleDto param);

        Task<PagedResultDto<IdentityRoleDto>> GetPageListAsync(int pageNo, int pageSize, QueryIdentityRoleDto param);
    }
}

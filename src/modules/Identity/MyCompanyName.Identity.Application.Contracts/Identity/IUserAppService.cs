using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Identity
{
    public interface IUserAppService : IApplicationService
    {
        Task<List<IdentityRoleDto>> GetRolesAsync(Guid id);

        Task<List<IdentityRoleDto>> GetAssignableRolesAsync();

        Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);

        Task<IdentityUserDto> FindByUsernameAsync(string userName);

        Task<IdentityUserDto> FindByEmailAsync(string email);


        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);

        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input);

        Task DeleteAsync(Guid id);

        Task<IdentityUserDto> GetAsync(Guid id);

        Task<List<IdentityUserDto>> GetListAsync(QueryIdentityUserDto param);

        Task<PagedResultDto<IdentityUserDto>> GetPageListAsync(int pageNo, int pageSize, QueryIdentityUserDto param);
    }
}

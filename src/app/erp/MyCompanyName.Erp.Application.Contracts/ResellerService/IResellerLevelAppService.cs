using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp.ResellerService
{
    public interface IResellerLevelAppService : IApplicationService
    {
        Task<Guid?> CreateAsync(CreateResellerLevelDto param);

        Task<Guid?> UpdateAsync(Guid id, UpdateResellerLevelDto param);

        Task UpdateActiveAsync(Guid id, bool active);

        Task UpdateIsDefaultAsync(Guid id, bool isDefault);

        Task DeleteAsync(Guid id);

        Task DeleteManyAsync(List<Guid> ids);

        Task<ResellerLevelDto> GetAsync(Guid id);

        Task<List<ResellerLevelDto>> GetListAsync(QueryResellerLevelDto param);

        Task<PagedResultDto<ResellerLevelDto>> GetPageListAsync(int pageNo, int pageSize, QueryResellerLevelDto param);

    }
}

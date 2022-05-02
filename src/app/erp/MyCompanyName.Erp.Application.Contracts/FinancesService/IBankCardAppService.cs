using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp.FinancesService
{
    public interface IBankCardAppService : IApplicationService
    {
        Task<Guid?> CreateAsync(CreateBankCardDto param);

        Task<Guid?> UpdateAsync(Guid id, UpdateBankCardDto param);

        Task UpdateActiveAsync(Guid id, bool active);

        Task DeleteAsync(Guid id);

        Task<BankCardDto> GetAsync(Guid id);

        Task<List<BankCardDto>> GetListAsync(QueryBankCardDto param);

        Task<PagedResultDto<BankCardDto>> GetPageListAsync(int pageNo, int pageSize, QueryBankCardDto param);

    }
}

using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Erp.Entities.Finance;
using MyCompanyName.Erp.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MyCompanyName.Erp.FinancesService
{
    [Authorize(FinancePermissions.BankCard.Default)]
    public class BankCardAppService : ErpAppService, IBankCardAppService
    {
        private readonly IRepository<BankCard, Guid> _bankCardRepository;
        public BankCardAppService(
            IRepository<BankCard, Guid> bankCardRepository
            )
        {
            _bankCardRepository = bankCardRepository;
        }

        public async Task<Guid?> CreateAsync(CreateBankCardDto param)
        {
            var hasAccountNo = await _bankCardRepository.AnyAsync(a => a.AccountNo == param.AccountNo);
            if (hasAccountNo)
            {
                throw new BusinessException(ErpErrorCodes.AlreadyExists)
                    .WithData("Name", "卡号/账号")
                    .WithData("Value", param.AccountNo);
            }
            var dbEntity = ObjectMapper.Map<CreateBankCardDto, BankCard>(param);
            dbEntity = await _bankCardRepository.InsertAsync(dbEntity);
            return dbEntity?.Id;
        }

        public async Task<Guid?> UpdateAsync(Guid id, UpdateBankCardDto param)
        {
            var query = await _bankCardRepository.FindAsync(f => f.Id == id);
            ObjectMapper.Map(param, query);
            query = await _bankCardRepository.UpdateAsync(query);
            return query?.Id;
        }

        public async Task UpdateActiveAsync(Guid id, bool active)
        {
            var query = await _bankCardRepository.FindAsync(f => f.Id == id);
            if (query != null && query.IsActive != active)
            {
                query.IsActive = active;
                await _bankCardRepository.UpdateAsync(query);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bankCardRepository.DeleteAsync(id);
        }

        public async Task<BankCardDto> GetAsync(Guid id)
        {
            var query = await _bankCardRepository.FindAsync(f => f.Id == id);
            var resut = ObjectMapper.Map<BankCard, BankCardDto>(query);
            return resut;
        }

        public async Task<List<BankCardDto>> GetListAsync(QueryBankCardDto param)
        {
            var query = GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToListAsync(query);
            var resutList = ObjectMapper.Map<List<BankCard>, List<BankCardDto>>(queryList);
            return resutList;
        }

        public async Task<PagedResultDto<BankCardDto>> GetPageListAsync(int pageNo, int pageSize, QueryBankCardDto param)
        {
            var query = GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToPageListAsync(pageNo, pageSize, query);
            var resutPageList = ObjectMapper.Map<PagedResultDto<BankCard>, PagedResultDto<BankCardDto>>(queryList);
            return resutPageList;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IQueryable<BankCard> GetQueryableAsync(QueryBankCardDto param)
        {
            var dbPageList = _bankCardRepository
             .WhereIf(!param.AccountNo.IsNullOrEmpty(), w => w.AccountNo.Contains(param.AccountNo))
             .WhereIf(!param.BankName.IsNullOrEmpty(), w => w.BankName.Contains(param.BankName))
             .WhereIf(!param.RealName.IsNullOrEmpty(), w => w.RealName.Contains(param.RealName))
             .WhereIf(!param.KeyWord.IsNullOrEmpty(), w => w.RealName.Contains(param.RealName) || w.AccountNo.Contains(param.AccountNo) || w.RealName.Contains(param.RealName))
             .WhereIf(param.IsActive.HasValue, w => w.IsActive == param.IsActive);
            return dbPageList;
        }
    }
}

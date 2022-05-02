using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Erp.Entities.Reseller;
using MyCompanyName.Erp.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MyCompanyName.Erp.ResellerService
{
    [Authorize(ResellerPermissions.Level.Default)]
    public class ResellerLevelService : ErpAppService, IResellerLevelAppService
    {
        private readonly IRepository<ResellerLevel, Guid> _levelRepository;

        public ResellerLevelService(
            IRepository<ResellerLevel, Guid> levelRepository
            )
        {
            _levelRepository = levelRepository;
        }

        [Authorize(ResellerPermissions.Level.Create)]
        public async Task<Guid?> CreateAsync(CreateResellerLevelDto param)
        {
            var dbEntity = ObjectMapper.Map<CreateResellerLevelDto, ResellerLevel>(param);
            var hasName = await _levelRepository.AnyAsync(f => f.Name == param.Name);
            if (hasName)
            {
                throw new BusinessException(ErpErrorCodes.AlreadyExists)
                    .WithData("Name", "卡号/账号")
                    .WithData("Value", param.Name);
            }
            dbEntity = await _levelRepository.InsertAsync(dbEntity);
            return dbEntity?.Id;
        }

        [Authorize(ResellerPermissions.Level.Update)]
        public async Task<Guid?> UpdateAsync(Guid id, UpdateResellerLevelDto param)
        {
            var query = await _levelRepository.FindAsync(f => f.Id == id);
            ObjectMapper.Map(param, query);
            query = await _levelRepository.UpdateAsync(query);
            return query?.Id;
        }

        [Authorize(ResellerPermissions.Level.Update)]
        public async Task UpdateActiveAsync(Guid id, bool active)
        {
            var query = await _levelRepository.FindAsync(f => f.Id == id);
            if (query != null && query.IsActive != active)
            {
                query.IsActive = active;
                await _levelRepository.UpdateAsync(query);
            }
            await Task.CompletedTask;
        }

        [Authorize(ResellerPermissions.Level.Update)]
        public async Task UpdateIsDefaultAsync(Guid id, bool isDefault)
        {
            var query = _levelRepository.WhereIf(isDefault, w => w.Id == id || w.IsDefault).WhereIf(!isDefault, w => w.Id == id);
            var queryList = await AsyncExecuter.ToListAsync(query);
            if (queryList.Any())
            {
                queryList.ForEach(f => f.IsDefault = (f.Id == id) && isDefault);
                await _levelRepository.UpdateManyAsync(queryList);
            }
        }
        [Authorize(ResellerPermissions.Level.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _levelRepository.DeleteAsync(id);
        }

        [Authorize(ResellerPermissions.Level.Delete)]
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            await _levelRepository.DeleteManyAsync(ids);
        }

        public async Task<ResellerLevelDto> GetAsync(Guid id)
        {
            var query = await _levelRepository.FindAsync(f => f.Id == id);
            var resut = ObjectMapper.Map<ResellerLevel, ResellerLevelDto>(query);
            return resut;
        }

        public async Task<List<ResellerLevelDto>> GetListAsync(QueryResellerLevelDto param)
        {
            var query = await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToListAsync(query);
            var resutList = ObjectMapper.Map<List<ResellerLevel>, List<ResellerLevelDto>>(queryList);
            return resutList;
        }

        public async Task<PagedResultDto<ResellerLevelDto>> GetPageListAsync(int pageNo, int pageSize, QueryResellerLevelDto param)
        {
            var query = await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToPageListAsync(pageNo, pageSize, query);
            var resutPageList = ObjectMapper.Map<PagedResultDto<ResellerLevel>, PagedResultDto<ResellerLevelDto>>(queryList);
            return resutPageList;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected async Task<IQueryable<ResellerLevel>> GetQueryableAsync(QueryResellerLevelDto param)
        {
            var dbPageList = (await _levelRepository.GetQueryableAsync())
             .WhereIf(!param.Name.IsNullOrEmpty(), w => w.Name.Contains(param.Name))
             .WhereIf(param.IsActive.HasValue, w => w.IsActive == param.IsActive)
             .OrderByDescending(o => o.IsDefault)
             .ThenByDescending(o => o.CreationTime);
            return dbPageList;
        }
    }
}

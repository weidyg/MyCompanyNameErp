using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Abp.Security;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MyCompanyName.Erp.ResellerService
{
    [Authorize(ResellerPermissions.Reseller.Default)]
    public class ResellerInfoService : ErpAppService, IResellerInfoAppService
    {
        protected ICompanyRepository CompanyRepository { get; }
        public ResellerInfoService(
            ICompanyRepository companyRepository
            )
        {
            CompanyRepository = companyRepository;
        }

        /// <summary>
        /// 获取分销商分页列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ResellerInfoDto>> GetPageListAsync(int pageNo, int pageSize, QueryResellerInfoDto param)
        {
            var query =await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToPageListAsync(pageNo, pageSize, query);
            var resutPageList = ObjectMapper.Map<PagedResultDto<Company>, PagedResultDto<ResellerInfoDto>>(queryList);
            return resutPageList;
        }


        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected async Task<IQueryable<Company>> GetQueryableAsync(QueryResellerInfoDto param)
        {
            var queryable = (await CompanyRepository.GetQueryableAsync())
             .Where(w => w.LinkTenants.Any(a => a.TenantId == CurrentTenant.Id && a.ClientType == ClientType.EFX.ToString()))
             .WhereIf(!param.Name.IsNullOrEmpty(), w => w.Name.Contains(param.Name))
             //.WhereIf(param.IsActive.HasValue, w => w.IsActive == param.IsActive)
             .OrderByDescending(o => o.CreationTime);
            return queryable;
        }
    }
}

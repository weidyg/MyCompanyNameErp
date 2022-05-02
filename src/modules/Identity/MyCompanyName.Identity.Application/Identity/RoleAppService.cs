using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyCompanyName.Identity.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Linq;
using Volo.Abp.ObjectExtending;

namespace MyCompanyName.Identity
{
    [Authorize(IdentityPermissions.Roles.Default)]
    public class RoleAppService : IdentityAppService, IRoleAppService
    {
        protected IdentityRoleManager RoleManager { get; }
        protected IIdentityRoleRepository RoleRepository { get; }

        public RoleAppService(
            IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository)
        {
            RoleManager = roleManager;
            RoleRepository = roleRepository;
        }


        [Authorize(IdentityPermissions.Roles.Create)]
        public async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            var role = new IdentityRole(GuidGenerator.Create(), input.Name, CurrentTenant.Id)
            {
                IsDefault = input.IsDefault,
                IsPublic = input.IsPublic
            };
            input.MapExtraPropertiesTo(role);
            (await RoleManager.CreateAsync(role)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(role);
        }

        [Authorize(IdentityPermissions.Roles.Update)]
        public async Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            var role = await RoleManager.GetByIdAsync(id);
            //role.ConcurrencyStamp = input.ConcurrencyStamp;
            (await RoleManager.SetRoleNameAsync(role, input.Name)).CheckErrors();
            role.IsDefault = input.IsDefault;
            role.IsPublic = input.IsPublic;
            input.MapExtraPropertiesTo(role);
            (await RoleManager.UpdateAsync(role)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(role);
        }

        [Authorize(IdentityPermissions.Roles.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var role = await RoleManager.FindByIdAsync(id.ToString());
            if (role == null) { return; }
            (await RoleManager.DeleteAsync(role)).CheckErrors();
        }


        public async Task<IdentityRoleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityRole, IdentityRoleDto>(
                await RoleManager.GetByIdAsync(id)
            );
        }

        public async Task<List<IdentityRoleDto>> GetListAsync(QueryIdentityRoleDto param)
        {
            var query =await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToListAsync(query);
            var resutList = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(queryList);
            return resutList;
        }
        public async Task<PagedResultDto<IdentityRoleDto>> GetPageListAsync(int pageNo, int pageSize, QueryIdentityRoleDto param)
        {
            var query =await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToPageListAsync(pageNo, pageSize, query);
            var resutPageList = ObjectMapper.Map<PagedResultDto<IdentityRole>, PagedResultDto<IdentityRoleDto>>(queryList);
            return resutPageList;
        }

        #region 
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected async Task<IQueryable<IdentityRole>> GetQueryableAsync(QueryIdentityRoleDto param)
        {
            var queryable = await RoleRepository.GetQueryableAsync();
            queryable = queryable
             .WhereIf(!param.Name.IsNullOrEmpty(), w => w.Name.Contains(param.Name))
             //.WhereIf(param.IsActive.HasValue, w => w.IsActive == param.IsActive)
             .OrderByDescending(o => o.IsDefault)
             //.ThenByDescending(o => o.IsActive)
             ;
            return queryable;
        }

        #endregion
    }
}

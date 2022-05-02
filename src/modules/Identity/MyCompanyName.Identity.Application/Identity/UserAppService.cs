using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyCompanyName.Identity.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Linq;
using Volo.Abp.ObjectExtending;

namespace MyCompanyName.Identity
{
    [Authorize(IdentityPermissions.Users.Default)]
    public class UserAppService : IdentityAppService, IUserAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }

        public UserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IOptions<IdentityOptions> identityOptions)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
        }

        [Authorize(IdentityPermissions.Users.Create)]
        public async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            await IdentityOptions.SetAsync();
            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.UserName,
                CurrentCompany.Id
            );
            input.MapExtraPropertiesTo(user);
            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        [Authorize(IdentityPermissions.Users.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            if (CurrentUser.Id == id) { throw new BusinessException(code: IdentityErrorCodes.UserSelfDeletion); }
            var user = await UserManager.FindByIdAsync(id.ToString()); if (user == null) { return; }
            if (user.IsSystemAdminUser()) { throw new BusinessException(code: IdentityErrorCodes.SystemAdminUserDeletion); }
            (await UserManager.DeleteAsync(user)).CheckErrors();
        }

        public async Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
              await UserManager.FindByEmailAsync(email)
          );
        }

        public async Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByNameAsync(userName)
            );
        }

        [Authorize(IdentityPermissions.Roles.Default)]
        public async Task<List<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            var list = await RoleRepository.GetListAsync();
            return ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list);
        }

        public async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.GetByIdAsync(id)
            );
        }

        public async Task<List<IdentityUserDto>> GetListAsync(QueryIdentityUserDto param)
        {
            var query =await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToListAsync(query);
            var resutList = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(queryList);
            return resutList;
        }

        public async Task<PagedResultDto<IdentityUserDto>> GetPageListAsync(int pageNo, int pageSize, QueryIdentityUserDto param)
        {
            var query = await GetQueryableAsync(param);
            var queryList = await AsyncExecuter.ToPageListAsync(pageNo, pageSize, query);
            var resutPageList = ObjectMapper.Map<PagedResultDto<IdentityUser>, PagedResultDto<IdentityUserDto>>(queryList);
            return resutPageList;
        }

        public async Task<List<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            //TODO: Should also include roles of the related OUs.
            var roles = await UserRepository.GetRolesAsync(id);
            return ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles);
        }

        [Authorize(IdentityPermissions.Users.Update)]
        public async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            await IdentityOptions.SetAsync();
            var user = await UserManager.GetByIdAsync(id);
            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();
            await UpdateUserByInput(user, input);
            input.MapExtraPropertiesTo(user);
            (await UserManager.UpdateAsync(user)).CheckErrors();
            //if (!input.Password.IsNullOrEmpty())
            //{
            //    (await UserManager.RemovePasswordAsync(user)).CheckErrors();
            //    (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            //}
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        [Authorize(IdentityPermissions.Users.Update)]
        public async Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            await UserRepository.UpdateAsync(user);
        }



        private async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }
            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }
            (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();
            user.Name = input.Name;
            user.Surname = input.Surname;
            (await UserManager.UpdateAsync(user)).CheckErrors();
            if (input.RoleNames != null)
            {
                (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected async Task<IQueryable<IdentityUser>> GetQueryableAsync(QueryIdentityUserDto param)
        {
            var queryable = await UserRepository.GetQueryableAsync();

            var dbPageList = queryable
                 .WhereIf(!param.FullName.IsNullOrEmpty(), u =>
                  (u.Name != null || u.Surname != null) && (u.Surname + u.Name).Contains(param.Filter)
                 )
                 .WhereIf(!param.UserName.IsNullOrEmpty(), w => w.UserName.Contains(param.UserName))
                 .WhereIf(!param.Filter.IsNullOrWhiteSpace(), u =>
                        u.UserName.Contains(param.Filter) ||
                        u.Email.Contains(param.Filter) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(param.Filter)) ||
                        ((u.Name != null || u.Surname != null) && (u.Surname + u.Name).Contains(param.Filter))
                    )
                 //.WhereIf(param.IsActive.HasValue, w => w.IsActive == param.IsActive)
                 .OrderByDescending(o => o.CreationTime)
                 ;
            return dbPageList;
        }

    }
}

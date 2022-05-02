using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MyCompanyName.Abp.DataFilter;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;

namespace MyCompanyName.Identity
{
    public class CompanyAppService : IdentityAppService, ICompanyAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        protected ICompanyRepository CompanyRepository { get; }
        protected IDistributedCache Cache { get; }

        private readonly IDataFilter _dataFilter;
        public CompanyAppService(
            IDataFilter dataFilter,
            IdentityUserManager userManager,
            IOptions<IdentityOptions> identityOptions,
            ICompanyRepository companyRepository,
            IDistributedCache cache)
        {
            _dataFilter = dataFilter;
            UserManager = userManager;
            IdentityOptions = identityOptions;
            Cache = cache;
            CompanyRepository = companyRepository;
        }

        [AllowAnonymous]
        public async Task ApplyAsync(ApplyCompanyDto input)
        {
            using (_dataFilter.Disable<IMultiCompany>())
            {
                var _cacheKey = $"Sms:{input.SmsNo}_{input.PhoneNumber}";
                var captcha = await Cache.GetStringAsync(_cacheKey);
                if (captcha != input.SmsCaptcha) { throw new UserFriendlyException("短信验证码错误！"); }
                var existName = await CompanyRepository.ExistNameAsync(input.CompanyName);
                if (existName) { throw new UserFriendlyException("公司名称已被注册！"); }
                var existPhone = await UserManager.ExistPhoneNumberAsync(input.PhoneNumber);
                if (existPhone) { throw new UserFriendlyException("手机号码已被注册！"); }
                var existEmail = await UserManager.ExistEmailAsync(input.Email);
                if (existEmail) { throw new UserFriendlyException("邮箱已被注册！"); }

                var companyEto = new Company(GuidGenerator.Create(), input.CompanyName);
                await CompanyRepository.InsertAsync(companyEto);
                await IdentityOptions.SetAsync();
                var user = new IdentityUser(GuidGenerator.Create(), input.UserName, companyEto.Id);
                (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
                if (!input.Email.IsNullOrWhiteSpace())
                {
                    (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
                }
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
                (await UserManager.SetLockoutEnabledAsync(user, true)).CheckErrors();
                user.Name = input.Name;
                user.Surname = input.Surname;
                user.SetSystemAdmin(GuidGenerator);
                (await UserManager.UpdateAsync(user)).CheckErrors();
                await Cache.RemoveAsync(_cacheKey);
            }
        }
    }
}

using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Validation;

namespace MyCompanyName.Identity
{
    public class ApplyCompanyDto
    {
        [Required]
        [DynamicStringLength(typeof(IdentityCompanyConsts), nameof(IdentityCompanyConsts.MaxNameLength))]
        public string CompanyName { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        public string UserName { get; set; }

        [DisableAuditing]
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string Password { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
        public string Surname { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
        public string Name { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        public string Email { get; set; }

        /// <summary>
        /// 短信编号
        /// </summary>
        [Required]
        public string SmsNo { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required]
        public string SmsCaptcha { get; set; }

    }
}
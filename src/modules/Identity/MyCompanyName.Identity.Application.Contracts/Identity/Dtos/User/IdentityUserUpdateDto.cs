using Volo.Abp.Auditing;
using Volo.Abp.Validation;


namespace MyCompanyName.Identity
{
    public class IdentityUserUpdateDto : IdentityUserCreateOrUpdateDtoBase
    {
        //[DisableAuditing]
        //[DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        //public string Password { get; set; }
    }
}
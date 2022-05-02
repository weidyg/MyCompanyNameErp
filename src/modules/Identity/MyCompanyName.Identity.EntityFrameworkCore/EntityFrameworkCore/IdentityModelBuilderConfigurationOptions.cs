using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MyCompanyName.Identity.EntityFrameworkCore
{
    public class IdentityModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public IdentityModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
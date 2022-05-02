using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MyCompanyName.TenantManagement.EntityFrameworkCore
{
    public class TenantModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public TenantModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
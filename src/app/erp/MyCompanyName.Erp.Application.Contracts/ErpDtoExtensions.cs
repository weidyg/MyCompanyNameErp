using Volo.Abp.Threading;

namespace MyCompanyName.Erp
{
    public static class ErpDtoExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /*可以向DTO添加扩展属性在依赖模块中定义。
                 * 示例：
                 * ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 * 有关更多信息，请参阅文档：
                 * https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions
                 */
            });
        }
    }
}
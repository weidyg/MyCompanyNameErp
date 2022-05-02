using Volo.Abp.Threading;

namespace MyCompanyName.Erp.ObjectExtending
{
    public static class ErpDomainObjectExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /*可以将扩展属性配置为实体或其他对象类型在依赖模块中定义。
                 * 如果您使用的是EF Core，并且希望将实体扩展属性映射到新的表字段，然后在MyCompanyName.ErpEfCoreEntityExtensionMappings中配置它们
                 * 示例：
                 *  ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityRole, string>("Title");
                 * 有关更多信息，请参阅文档：
                 * https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions
                 */
            });
        }
    }
}

namespace MyCompanyName.Erp.ResellerService
{
    public class QueryResellerLevelDto
    {
        /// <summary>
        /// 等级名称
        /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
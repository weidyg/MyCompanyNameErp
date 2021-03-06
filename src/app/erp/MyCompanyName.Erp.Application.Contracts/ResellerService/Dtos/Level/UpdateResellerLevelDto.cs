namespace MyCompanyName.Erp.ResellerService
{
    public class UpdateResellerLevelDto
    {  
        /// <summary>
       /// 等级名称
       /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// 等级折扣(0-1)
        /// </summary>  
        public decimal Discount { get; set; }

        /// <summary>
        /// 等级描述
        /// </summary>   
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
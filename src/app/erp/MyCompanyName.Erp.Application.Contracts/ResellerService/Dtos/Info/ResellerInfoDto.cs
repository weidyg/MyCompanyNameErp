using System;
using Volo.Abp.Application.Dtos;

namespace MyCompanyName.Erp.ResellerService
{
    public class ResellerInfoDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 公司名称
        /// </summary>   
        public string Name { get; set; }
    }
}
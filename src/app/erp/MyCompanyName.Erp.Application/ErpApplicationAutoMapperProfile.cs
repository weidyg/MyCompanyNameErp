using AutoMapper;
using MyCompanyName.Erp.Entities.Finance;
using MyCompanyName.Erp.Entities.Reseller;
using MyCompanyName.Erp.FinancesService;
using MyCompanyName.Erp.ResellerService;
using MyCompanyName.Identity;
using Volo.Abp.Application.Dtos;

namespace MyCompanyName.Erp
{
    public class ErpApplicationAutoMapperProfile : Profile
    {
        public ErpApplicationAutoMapperProfile()
        {
            /* 这里配置AutoMapper映射配置。*/
            CreateMap(typeof(PagedResultDto<>), typeof(PagedResultDto<>));

            CreateMap<UpdateBankCardDto, BankCard>();
            CreateMap<CreateBankCardDto, BankCard>();
            CreateMap<BankCard, BankCardDto>();

            CreateMap<UpdateResellerLevelDto, ResellerLevel>();
            CreateMap<CreateResellerLevelDto, ResellerLevel>();
            CreateMap<ResellerLevel, ResellerLevelDto>();
            CreateMap<Company, ResellerInfoDto>();
        }
    }
}

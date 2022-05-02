using AutoMapper;

namespace MyCompanyName.Identity
{
    public class IdentityApplicationAutoMapperProfile : Profile
    {
        public IdentityApplicationAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>()
              .MapExtraProperties();

            CreateMap<IdentityRole, IdentityRoleDto>()
                .MapExtraProperties();

            CreateMap<IdentityUser, ProfileDto>()
                .ForMember(dest => dest.HasPassword,
                    op => op.MapFrom(src => src.PasswordHash != null))
                .MapExtraProperties();
        }
    }
}
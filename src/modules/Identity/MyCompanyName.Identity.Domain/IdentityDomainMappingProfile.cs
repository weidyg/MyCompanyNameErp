using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompanyName.Identity
{
    public class IdentityDomainMappingProfile : Profile
    {
        public IdentityDomainMappingProfile()
        {
            //CreateMap<IdentityUser, UserEto>();
            //CreateMap<IdentityClaimType, IdentityClaimTypeEto>();
            CreateMap<IdentityRole, IdentityRoleEto>();
            CreateMap<OrganizationUnit, OrganizationUnitEto>();
        }
    }
}

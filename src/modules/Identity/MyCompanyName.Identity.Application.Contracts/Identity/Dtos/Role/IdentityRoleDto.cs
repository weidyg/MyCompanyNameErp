using System;
using Volo.Abp.Application.Dtos;

namespace MyCompanyName.Identity
{
    public class IdentityRoleDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public bool IsStatic { get; set; }

        public bool IsPublic { get; set; }
    }
}
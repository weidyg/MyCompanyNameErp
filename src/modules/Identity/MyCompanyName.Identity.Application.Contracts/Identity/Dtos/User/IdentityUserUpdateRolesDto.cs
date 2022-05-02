using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.Identity
{
    public class IdentityUserUpdateRolesDto
    {
        [Required]
        public string[] RoleNames { get; set; }
    }
}
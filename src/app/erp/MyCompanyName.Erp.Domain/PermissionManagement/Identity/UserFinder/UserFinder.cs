using MyCompanyName.Identity;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Erp.Permissions.Identity
{
    public class UserFinder : IUserFinder, ITransientDependency
    {
        protected IIdentityUserRepository IdentityUserRepository { get; }

        public UserFinder(
            IIdentityUserRepository identityUserRepository
            )
        {
            IdentityUserRepository = identityUserRepository;
        }
        public virtual async Task<bool> IsSystemAdminAsync(Guid userId)
        {
            return (await IdentityUserRepository.FindAsync(userId))?.IsSystemAdminUser() ?? false;
        }
        public virtual async Task<string[]> GetRolesAsync(Guid userId)
        {
            return (await IdentityUserRepository.GetRoleNamesAsync(userId)).ToArray();
        }
    }
}

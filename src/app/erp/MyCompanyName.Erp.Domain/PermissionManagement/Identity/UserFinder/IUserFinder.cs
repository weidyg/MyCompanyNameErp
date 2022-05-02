using System;
using System.Threading.Tasks;

namespace MyCompanyName.Erp.Permissions.Identity
{
    public interface IUserFinder
    {
        Task<string[]> GetRolesAsync(Guid userId);

        Task<bool> IsSystemAdminAsync(Guid userId);
    }
}

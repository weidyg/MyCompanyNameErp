using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Identity
{
    public interface ICompanyAppService : IApplicationService
    {
        Task ApplyAsync(ApplyCompanyDto input);
    }
}

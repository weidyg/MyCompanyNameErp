using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Efx.Web.Pages
{
    [AllowAnonymous]
    public class DemoModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DemoModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

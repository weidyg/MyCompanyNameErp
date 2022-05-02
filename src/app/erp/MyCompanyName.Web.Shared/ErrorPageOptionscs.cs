using System.Collections.Generic;

namespace MyCompanyName.Web.Shared
{
    public class ErrorPageOptions
    {
        public readonly IDictionary<string, string> ErrorViewUrls;

        public ErrorPageOptions()
        {
            ErrorViewUrls = new Dictionary<string, string>();
        }
    }
}

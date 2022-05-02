using Volo.Abp.Http;

namespace MyCompanyName.Erp.Web.Views.Error
{
    public class ErrorViewModel
    {
        public RemoteServiceErrorInfo ErrorInfo { get; set; }

        public int HttpStatusCode { get; set; }
    }
}

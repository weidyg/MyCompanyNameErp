using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MyCompanyName.Erp.Web.Views.Error;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.ExceptionHandling;

namespace MyCompanyName.Web.Shared.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : AbpController
    {
        private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
        private readonly IStringLocalizer<AbpUiResource> _localizer;
        private readonly ErrorPageOptions _abpErrorPageOptions;
        private readonly IExceptionNotifier _exceptionNotifier;
        private readonly AbpExceptionHandlingOptions _exceptionHandlingOptions;

        public ErrorController(
            IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
            IHttpExceptionStatusCodeFinder httpExceptionStatusCodeFinder,
            IOptions<ErrorPageOptions> abpErrorPageOptions,
            IStringLocalizer<AbpUiResource> localizer,
            IExceptionNotifier exceptionNotifier,
            IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions)
        {
            _errorInfoConverter = exceptionToErrorInfoConverter;
            _statusCodeFinder = httpExceptionStatusCodeFinder;
            _localizer = localizer;
            _exceptionNotifier = exceptionNotifier;
            _exceptionHandlingOptions = exceptionHandlingOptions.Value;
            _abpErrorPageOptions = abpErrorPageOptions.Value;
        }

        [Route("[controller]/{httpStatusCode}")]
        public async Task<IActionResult> Index(int httpStatusCode)
        {
            var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = exHandlerFeature != null
                ? exHandlerFeature.Error
                : (new List<int> { 401, 403, 404 }).Contains(httpStatusCode)
                ? new Exception(_localizer[$"DefaultErrorMessage{httpStatusCode}"], new NotSupportedException(_localizer[$"DefaultErrorMessage{httpStatusCode}Detail"]))
                : new Exception(_localizer["UnhandledException"]);

            await _exceptionNotifier.NotifyAsync(new ExceptionNotificationContext(exception));

            var errorInfo = _errorInfoConverter.Convert(exception, _exceptionHandlingOptions.SendExceptionsDetailsToClients);

            if (httpStatusCode == 0)
            {
                httpStatusCode = (int)_statusCodeFinder.GetStatusCode(HttpContext, exception);
            }

            HttpContext.Response.StatusCode = httpStatusCode;

            var page = GetErrorPageUrl(httpStatusCode);

            return View(page, new ErrorViewModel
            {
                ErrorInfo = errorInfo,
                HttpStatusCode = httpStatusCode
            });
        }

        private string GetErrorPageUrl(int statusCode)
        {
            var page = _abpErrorPageOptions.ErrorViewUrls.GetOrDefault(statusCode.ToString());

            if (string.IsNullOrWhiteSpace(page))
            {
                return "~/Views/Error/Default.cshtml";
            }

            return page;
        }
    }
}

using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using rendezvousBistro.Api.Common.Http;

namespace rendezvousBistro.Api.Common.Errors
{
    public class RendezvousBistroProblemDetailFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null
    ) : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options = options?
            .Value ?? throw new ArgumentNullException(nameof(options));
        private readonly Action<ProblemDetailsContext>? _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            ArgumentNullException.ThrowIfNull(modelStateDictionary);

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }
            // Customizing the problem details is the last thing we do,
            // so that the developer can overwrite the defaults.
            var errors = httpContext?.Items["Errors"] as List<Error>;
            if (errors is not null)
            {
                problemDetails.Extensions.Add(HttpContextItemKeys.Errors, errors.Select(e => e.Code));
            }

            _configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
        }
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace rendezvousBistro.Application.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        // var errorResult = new {error = "Filter: An error occur while processing your request"};
        var problemDetail = new ProblemDetails
        {
            Title = "Filter + Problem Detail: An error occur while processing your request",
            Instance = context.HttpContext.Request.Path,
            Status = (int)HttpStatusCode.InternalServerError,
        };
        context.Result = new ObjectResult(problemDetail);
        context.ExceptionHandled = true;
        // base.OnException(context);
    }
}
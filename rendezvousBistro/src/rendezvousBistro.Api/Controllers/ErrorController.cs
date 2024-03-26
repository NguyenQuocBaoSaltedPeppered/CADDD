using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace rendezvousBistro.Api.Controllers;

/// <summary>
/// Error Controller
/// </summary>
public class ErrorController : ControllerBase
{
    /// <summary>
    /// Error handler
    /// </summary>
    /// <returns></returns>
    [Route("/error")]
    [NonAction]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(
            title: exception?.Message
            // detail: exception?.StackTrace
        );
    }
}
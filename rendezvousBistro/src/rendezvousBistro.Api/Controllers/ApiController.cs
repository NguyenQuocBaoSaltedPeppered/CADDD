using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using rendezvousBistro.Api.Common.Http;

namespace rendezvousBistro.Api.Controllers;

/// <summary>
/// Base controller for the system
/// </summary>
[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    /// <summary>
    /// Problem controller
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if(errors.Count == 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        return Problem(errors[0]);
    }

    /// <summary>
    /// Problem handler for a single error
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    private ObjectResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    /// <summary>
    /// Problem handler for multiple errors
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    private ActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description
            );
        }

        return ValidationProblem(modelStateDictionary);
    }
}
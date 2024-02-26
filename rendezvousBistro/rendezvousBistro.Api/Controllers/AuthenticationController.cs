using FluentResults;
using Microsoft.AspNetCore.Mvc;
using rendezvousBistro.Application.Common.Errors;
using rendezvousBistro.Application.Services.Authentication;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        Result<AuthenticationResult> registerResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        if(registerResult.IsSuccess)
        {
            return Ok(MapAuthResult(registerResult.Value));
        }
        var firstError = registerResult.Errors[0];
        if(firstError is DuplicateEmailError)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                detail: "Email already exists"
            );
        }
        return Problem();
    }
    private static AuthenticationResponse MapAuthResult(AuthenticationResult result) =>
        new(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password
        );
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        return Ok(response);
    }
}
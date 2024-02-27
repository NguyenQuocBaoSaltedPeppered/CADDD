using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using rendezvousBistro.Application.Services.Authentication;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Controllers;

[Route("auth")]
public class AuthenticationController(
    IAuthenticationService authenticationService
) : ApiController
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result) =>
        new(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        // ErrorOr.MatchFirst is a method that takes two functions as arguments.
        // The first function is called if the ErrorOr is a Result with a value,
        // and the second function is called if the ErrorOr is `an Error`.
        //
        // return registerResult.MatchFirst(
        //     authResult => Ok(MapAuthResult(authResult)),
        //     firstError => Problem(
        //         statusCode: StatusCodes.Status409Conflict,
        //         title: firstError.Description
        //     )
        // );
        // ---
        // ErrorOr.Match is a method that takes two functions as arguments.
        // The first function is called if the ErrorOr is a Result with a value,
        // and the second function is called if the ErrorOr is `Errors`.
        //
        // return registerResult.Match(
        //     authResult => Ok(MapAuthResult(authResult)),
        //     _ => Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists")
        // );
        // ---
        // Match using with custom Problem from ApiController
        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            Problem
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(
            request.Email,
            request.Password
        );
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            Problem
        );
    }
}
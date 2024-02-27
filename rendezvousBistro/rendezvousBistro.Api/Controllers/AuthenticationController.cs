using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using rendezvousBistro.Application.Authentication.Commands.Register;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Authentication.Queries.Login;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Controllers;

[Route("auth")]
public class AuthenticationController(
    IMediator mediator
    // IAuthenticationCommandService authenticationCommandService,
    // IAuthenticationQueryService authenticationQueryService
) : ApiController
{
    private readonly IMediator _mediator = mediator;
    // private readonly IAuthenticationCommandService _authenticationCommandService = authenticationCommandService;
    // private readonly IAuthenticationQueryService _authenticationQueryService = authenticationQueryService;

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result) =>
        new(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);
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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );
        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);
        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            Problem
        );
    }
}
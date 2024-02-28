using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using rendezvousBistro.Application.Authentication.Commands.Register;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Authentication.Queries.Login;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Controllers;

[Route("auth")]
public class AuthenticationController(
    ISender mediator,
    IMapper mapper
) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // var command = new RegisterCommand(
        //     request.FirstName,
        //     request.LastName,
        //     request.Email,
        //     request.Password
        // );
        // Map item with mapster
        var command = _mapper.Map<RegisterCommand>(request);
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
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);
        return loginResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem
        );
    }
}
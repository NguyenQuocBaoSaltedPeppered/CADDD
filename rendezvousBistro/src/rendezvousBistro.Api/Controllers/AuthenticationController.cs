using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rendezvousBistro.Application.Authentication.Commands.Register;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Authentication.Queries.Login;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[Route("auth")]
[AllowAnonymous]
public class AuthenticationController(
    ISender mediator,
    IMapper mapper
) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request">Register request</param>
    /// <remarks>
    /// 
    ///     POST /auth/register
    /// 
    /// </remarks>
    /// <response code = "200">New user information</response>
    /// <response code = "500">Internal server error</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Map item with mapster
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem
        );
    }

    /// <summary>
    /// Login for a user
    /// </summary>
    /// <param name="request">Login request</param>
    /// <remarks>
    /// 
    ///     POST /auth/login
    /// 
    /// </remarks>
    /// <response code = "200">New user information</response>
    /// <response code = "500">Internal server error</response>
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
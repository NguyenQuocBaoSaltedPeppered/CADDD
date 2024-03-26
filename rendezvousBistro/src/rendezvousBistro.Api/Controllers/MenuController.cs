using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using rendezvousBistro.Application.Menus.Commands;
using rendezvousBistro.Contracts.Menus;

namespace rendezvousBistro.Api.Controllers;

/// <summary>
/// Menus controller
/// </summary>
[Route("hosts/{hostId}/menus")]
public class MenuController(
    ISender mediator,
    IMapper mapper
) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Create new menu
    /// </summary>
    /// <param name="hostId">Id of host</param>
    /// <param name="request">Create menu request</param>
    /// <remarks>
    /// 
    ///     POST /hosts/{hostId}/menus
    /// 
    /// </remarks>
    /// <response code = "200">New menu information</response>
    /// <response code = "500">Internal server error</response>
    [HttpPost]
    public async Task<IActionResult> CreateMenu(Guid hostId, CreateMenuRequest request)
    {
        var command = _mapper.Map<CreateMenuCommand>((request, hostId));
        var result = await _mediator.Send(command);
        return result.Match(
            menus => Ok(_mapper.Map<MenuResponse>(menus)),
            Problem);
    }
}
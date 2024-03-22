using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using rendezvousBistro.Application.Menus.Commands;
using rendezvousBistro.Contracts.Menus;

namespace rendezvousBistro.Api.Controllers;

[Route("hosts/{hostId}/menus")]
public class MenuController(
    ISender mediator,
    IMapper mapper
) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<IActionResult> CreateMenu(Guid hostId, CreateMenuRequest request)
    {
        try
        {
            var command = _mapper.Map<CreateMenuCommand>((request, hostId));
        var result = await _mediator.Send(command);
        return result.Match(
            menus => Ok(_mapper.Map<MenuResponse>(menus)),
            Problem);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
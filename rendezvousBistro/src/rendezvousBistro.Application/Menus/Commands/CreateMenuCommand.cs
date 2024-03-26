using ErrorOr;

using MediatR;

using rendezvousBistro.Domain.MenuAggregate;

namespace rendezvousBistro.Application.Menus.Commands;

/// <summary>
/// Create menu command
/// </summary>
/// <param name="HostId"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Sections"></param>
public record CreateMenuCommand(
    Guid HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections
) : IRequest<ErrorOr<Menu>>;

/// <summary>
/// Menu section command
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Items"></param>
public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items
);

/// <summary>
/// Menu item command
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <returns></returns>
public record MenuItemCommand(
    string Name,
    string Description
);
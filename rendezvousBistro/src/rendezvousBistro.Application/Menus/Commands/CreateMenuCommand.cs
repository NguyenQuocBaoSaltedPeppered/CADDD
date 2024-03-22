using ErrorOr;

using MediatR;

using rendezvousBistro.Domain.MenuAggregate;

namespace rendezvousBistro.Application.Menus.Commands;

public record CreateMenuCommand(
    Guid HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections
) : IRequest<ErrorOr<Menu>>;

public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items
);

public record MenuItemCommand(
    string Name,
    string Description
);
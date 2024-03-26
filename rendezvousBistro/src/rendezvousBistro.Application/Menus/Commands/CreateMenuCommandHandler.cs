using ErrorOr;

using MediatR;

using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.HostAggregate.ValueObjects;
using rendezvousBistro.Domain.MenuAggregate;
using rendezvousBistro.Domain.MenuAggregate.Entities;

namespace rendezvousBistro.Application.Menus.Commands;

/// <summary>
/// Create menu command handler
/// </summary>
/// <param name="menuRepository">Menu repository</param>
public class CreateMenuCommandHandler
(
    IMenuRepository menuRepository
) : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;

    /// <summary>
    /// Handle create menu command
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create Menu
        var menu = Menu.CreateNew(
            name: request.Name,
            description: request.Description,
            hostId: HostId.Create(request.HostId),
            sections: request.Sections.ConvertAll(s => MenuSection.CreateNew(
                name: s.Name,
                description: s.Description,
                items: s.Items.ConvertAll(i => MenuItem.CreateNew(
                    name: i.Name,
                    description: i.Description)))));

        // Persist Menu
        _menuRepository.Add(menu);

        // Return Menu
        return menu;
    }
}
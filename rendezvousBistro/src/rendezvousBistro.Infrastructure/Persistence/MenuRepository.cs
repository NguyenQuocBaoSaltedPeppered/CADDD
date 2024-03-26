using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.MenuAggregate;

namespace rendezvousBistro.Infrastructure.Persistence;

/// <inheritdoc cref="IMenuRepository"/>
public class MenuRepository : IMenuRepository
{
    private static readonly List<Menu> _menus = [];
    public void Add(Menu menu)
    {
        _menus.Add(menu);
    }
}
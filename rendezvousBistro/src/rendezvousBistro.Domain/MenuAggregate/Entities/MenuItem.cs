using rendezvousBistro.Domain.Common.Models;
using rendezvousBistro.Domain.MenuAggregate.ValueObjects;

namespace rendezvousBistro.Domain.MenuAggregate.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    private MenuItem(MenuItemId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem CreateNew(string name, string description)
    {
        return new MenuItem(MenuItemId.CreateUnique(), name, description);
    }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;
}
using rendezvousBistro.Domain.Common.Models;
using rendezvousBistro.Domain.MenuAggregate.ValueObjects;

namespace rendezvousBistro.Domain.MenuAggregate.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private MenuSection(
        MenuSectionId id,
        string name,
        string description,
        List<MenuItem> items) : base(id)
    {
        Name = name;
        Description = description;
        _items = items;
    }

    public static MenuSection CreateNew(
        string name,
        string description,
        List<MenuItem>? items = null)
    {
        return new MenuSection(
            MenuSectionId.CreateUnique(),
            name,
            description,
            items ?? []);
    }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    private readonly List<MenuItem> _items = [];

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();
}
using rendezvousBistro.Domain.Common.Models;
using rendezvousBistro.Domain.Common.ValueObjects;
using rendezvousBistro.Domain.DinnerAggregate.ValueObjects;
using rendezvousBistro.Domain.HostAggregate.ValueObjects;
using rendezvousBistro.Domain.MenuAggregate.Entities;
using rendezvousBistro.Domain.MenuAggregate.ValueObjects;
using rendezvousBistro.Domain.MenuReviewAggregate.ValueObjects;

namespace rendezvousBistro.Domain.MenuAggregate;

public sealed class Menu : AggregateRoot<MenuId>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public AverageRating AverageRating { get; private set; }
    private readonly List<MenuSection> _sections = [];
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    private readonly List<DinnerId> _dinnerIds = [];
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    private readonly List<MenuReviewId> _menuReviewIds = [];
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public HostId HostId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Menu(
        MenuId id,
        string name,
        string description,
        AverageRating averageRating,
        HostId hostId,
        DateTime created,
        DateTime updated,
        List<MenuSection> sections) : base(id)
    {
        Name = name;
        Description = description;
        AverageRating = averageRating;
        HostId = hostId;
        CreatedDateTime = created;
        UpdatedDateTime = updated;
        _sections = sections;
    }

    public static Menu CreateNew(
        string name,
        string description,
        HostId hostId,
        List<MenuSection>? sections = null)
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            AverageRating.CreateNew(),
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            sections ?? []);
    }
}
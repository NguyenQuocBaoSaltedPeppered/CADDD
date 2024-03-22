using rendezvousBistro.Domain.BillAggregate.ValueObjects;
using rendezvousBistro.Domain.Common.Models;
using rendezvousBistro.Domain.DinnerAggregate.ValueObjects;
using rendezvousBistro.Domain.GuestAggregate.ValueObjects;
using rendezvousBistro.Domain.HostAggregate.ValueObjects;

namespace rendezvousBistro.Domain.BillAggregate;

public sealed class Bill : AggregateRoot<BillId>
{
    public DinnerId DinnerId { get; }
    public GuestId GuestId { get; }
    public HostId HostId { get; }
    public Price Price { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }
    private Bill(
        BillId id,
        DinnerId dinnerId,
        GuestId guestId,
        HostId hostId,
        Price price,
        DateTime created,
        DateTime updated) : base(id)
    {
        DinnerId = dinnerId;
        GuestId = guestId;
        HostId = hostId;
        Price = price;
        CreatedDateTime = created;
        UpdatedDateTime = updated;
    }
    public static Bill CreateNew(
        DinnerId dinnerId,
        GuestId guestId,
        HostId hostId,
        Price price)
    {
        return new Bill(
            BillId.CreateUnique(),
            dinnerId,
            guestId,
            hostId,
            price,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
using rendezvousBistro.Domain.MenuAggregate;

namespace rendezvousBistro.Application.Common.Interfaces.Persistence;

public interface IMenuRepository
{
    void Add(Menu menu);
}
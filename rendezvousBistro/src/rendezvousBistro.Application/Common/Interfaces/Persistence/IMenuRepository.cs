using rendezvousBistro.Domain.MenuAggregate;

namespace rendezvousBistro.Application.Common.Interfaces.Persistence;

/// <summary>
/// Menu repository interface
/// </summary>
public interface IMenuRepository
{
    /// <summary>
    /// Add new menu
    /// </summary>
    /// <param name="menu">Menu</param>
    void Add(Menu menu);
}
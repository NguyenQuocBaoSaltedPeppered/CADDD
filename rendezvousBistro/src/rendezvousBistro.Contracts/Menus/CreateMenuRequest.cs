namespace rendezvousBistro.Contracts.Menus;

/// <summary>
/// Create menu request
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Sections"></param>
public record CreateMenuRequest(
    string Name,
    string Description,
    List<MenuSection> Sections
);

/// <summary>
/// Menu section request
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Items"></param>
/// <returns></returns>
public record MenuSection(
    string Name,
    string Description,
    List<MenuItem> Items
);

/// <summary>
/// Menu item request
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <returns></returns>
public record MenuItem(
    string Name,
    string Description
);
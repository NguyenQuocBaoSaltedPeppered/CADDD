namespace rendezvousBistro.Contracts.Menus;

/// <summary>
/// Menu response
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="AverageRating"></param>
/// <param name="Sections"></param>
/// <param name="HostId"></param>
/// <param name="DinnerIds"></param>
/// <param name="MenuReviewIds"></param>
/// <param name="CreatedDateTime"></param>
/// <param name="UpdatedDateTime"></param>
/// <returns></returns>
public record MenuResponse(
    Guid Id,
    string Name,
    string Description,
    double? AverageRating,
    List<MenuSectionResponse> Sections,
    string HostId,
    List<string> DinnerIds,
    List<string> MenuReviewIds,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

/// <summary>
/// Menu section response
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="Items"></param>
/// <returns></returns>
public record MenuSectionResponse(
    string Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items
);

/// <summary>
/// Menu item response
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record MenuItemResponse(
    string Id,
    string Name,
    string Description
);
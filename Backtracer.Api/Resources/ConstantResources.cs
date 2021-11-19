namespace Backtracer.Api.Resources;

/// <summary>
/// A group of Constants and their values
/// </summary>
public record struct ConstantResource(
    string Name,
    IDictionary<int, double> Values
);

/// <summary>
/// A single constant group without values
/// </summary>
public record struct ConstantGroupResource(
    int Id,
    string Name,
    DateTime CreatedAt
);

/// <summary>
/// A constant type
/// </summary>
public record struct ConstantTypeResource(
    int Id,
    string Description
);

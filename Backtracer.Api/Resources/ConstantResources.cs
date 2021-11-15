namespace Backtracer.Api.Resources;

public record struct ConstantResource(
    string Name,
    IDictionary<int, double> Values
);

public record struct ConstantGroupResource(
    int Id,
    string Name,
    DateTime CreatedAt
);

public record struct ConstantTypeResource(
    int Id,
    string Description
);

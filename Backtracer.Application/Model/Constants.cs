namespace Backtracer.Application.Model;

public record class ConstantType {
    public int Id { get; set; }
    public string Description { get; set; } = null!;
}

public record class ConstantGroup {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; }
}

public record class ConstantGroupWithValues {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; }
    public IDictionary<int, double> Constants { get; set; } = null!;
}

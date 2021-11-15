using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backtracer.Persistence.Model;

public record class ConstantEntity {
    public int ConstantGroupId { get; set; }
    public int ConstantTypeId { get; set; }
    public double Value { get; set; }

    public virtual ConstantGroupEntity ConstantGroup { get; set; } = null!;
    public virtual ConstantTypeEntity ConstantType { get; set; } = null!;
}

internal class ConstantEntityTypeConfiguration : IEntityTypeConfiguration<ConstantEntity> {
    private const string TableName = "Constants";
    private const string SchemaName = "tracer";

    public void Configure(EntityTypeBuilder<ConstantEntity> builder) {
        builder
            .ToTable(TableName, SchemaName)
            .HasKey(c => new { c.ConstantTypeId, c.ConstantGroupId });
    }
}

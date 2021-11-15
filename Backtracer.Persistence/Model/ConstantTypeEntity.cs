using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backtracer.Persistence.Model;

public record class ConstantTypeEntity {
    public int Id { get; set; }
    public string Description { get; set; } = null!;
}

internal class ConstantTypeEntityTypeConfiguration : IEntityTypeConfiguration<ConstantTypeEntity> {
    private const string TableName = "ConstantTypes";
    private const string SchemaName = "tracer";

    public void Configure(EntityTypeBuilder<ConstantTypeEntity> builder) {
        builder.ToTable(TableName, SchemaName);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backtracer.Persistence.Model;

public record class ConstantGroupEntity {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; }

    public virtual ICollection<ConstantEntity> Constants { get; set; } = new HashSet<ConstantEntity>();
}

internal class ConstantGroupEntityTypeConfiguration : IEntityTypeConfiguration<ConstantGroupEntity> {
    private const string TableName = "ConstantGroups";
    private const string SchemaName = "tracer";

    public void Configure(EntityTypeBuilder<ConstantGroupEntity> builder) {
        builder.ToTable(TableName, SchemaName);
    }
}

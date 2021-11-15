using System.Reflection;
using Backtracer.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace Backtracer.Persistence;

public class DataContext : DbContext {
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    public DbSet<ConstantEntity> Constants { get; set; } = null!;
    public DbSet<ConstantTypeEntity> ConstantTypes { get; set; } = null!;
    public DbSet<ConstantGroupEntity> ConstantGroups { get; set; } = null!;
}
